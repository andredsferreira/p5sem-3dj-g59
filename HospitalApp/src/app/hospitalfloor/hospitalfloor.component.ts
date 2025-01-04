import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import * as THREE from "three";
import * as GSAP from "gsap";
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import Ground from './jsfiles/ground';
import configJson from './config.json';
import Loader from './jsfiles/loader';
import RoomLoader from './jsfiles/roomloader';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HospitalFloorService } from './hospitalfloor-service';
import { Occupied, Room } from './types';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],   
  selector: 'app-hospitalfloor',
  templateUrl: './hospitalfloor.component.html',
  styleUrl: './hospitalfloor.component.css'
})
export class HospitalFloorComponent implements OnInit, OnDestroy {

  token!: string|null
  role: string = ""
  canRenderCanvas: boolean = false

  raycaster = new THREE.Raycaster();
  controls!: OrbitControls;

  showAmbientLightControls: boolean = false;
  showDirectionalLightControls: boolean = false;
  ambientIntensity: number = 0.5;
  ambientColor: string = "#ffffff";
  directionalIntensity: number = 3;
  directionalColor: string = "#ffffff";
  directionalLightPosition = { x: 5, y: 3, z: 5 };
  
  duration = 1.5;

  selectedDateTime: string = '';

  roomLoaders!: RoomLoader[]|null;

  rooms!: Room[]|null;
  selectedRoom: RoomLoader | null = null;
  showRoomInfo: boolean = false;

  private ambientLight!: THREE.AmbientLight;
  private directionalLight!: THREE.DirectionalLight;

  constructor(private auth: AuthService, private service: HospitalFloorService) { }

  ngOnDestroy(): void {
    this.scene.clear();
    //this.renderer.dispose();
  }

  async ngOnInit(): Promise<void> {
    this.token = localStorage.getItem('token');
    if (this.token) {
      this.role = this.auth.getRoleFromToken(this.token).toLowerCase();
      this.canRenderCanvas = this.role !== 'patient' && this.role !== "";
  
      if (this.canRenderCanvas) {
        try {
          const response = await this.service.getRooms(this.token);
          this.rooms = response.body;
          console.log(response.body);
          this.roomLoaders = [];
  
          // Cria a cena assim que os dados estão prontos
          this.createScene();
          this.render();
          this.selectedDateTime = this.getCurrentDateTime();
          this.handleDateTimeSelection();
          document.addEventListener("mousedown", this.onMouseDown.bind(this));
          document.addEventListener("keypress", this.iPressed.bind(this));
        } catch (error) {
          console.error("Error fetching rooms:", error);
        }
      }
    } else {
      console.warn('No token found in localStorage');
    }
  }

  @ViewChild('myCanvas') private canvasRef!: ElementRef;

  //* Stage Properties
  @Input() public cameraZ: number = 20;
  @Input() public cameraY: number = 10;
  @Input() public cameraX: number = 0;
  @Input() public fieldOfView: number = 30;
  @Input('nearClipping') public nearClippingPane: number = 1;
  @Input('farClipping') public farClippingPane: number = 100;

  //? Helper Properties (Private properties);
  private get canvas(): HTMLCanvasElement {
    return this.canvasRef.nativeElement;
  }
  private renderer!: THREE.WebGLRenderer;
  private scene: THREE.Scene = new THREE.Scene();
  private camera!: THREE.PerspectiveCamera;

  private getAspectRatio(): number {
    return this.canvas.clientWidth / this.canvas.clientHeight;
  }

  /**
  * Create the scene
  *
  * @private
  * @memberof HospitalFloorComponent
  */
  private createScene(): void {
    //* Scene
    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(0xfbeef2);

    //const axesHelpers = new THREE.AxesHelper(10);
    //this.scene.add(axesHelpers);

    var loaderInstance = new Loader({
      map: configJson.map, groundTextureUrl: configJson.floorTextureUrl,
      groundSize: { width: configJson.floorSize.width, height: configJson.floorSize.height },
      wallTextureUrl: configJson.wallTextureUrl,
      wallSize: { width: configJson.wallSize.width, height: configJson.wallSize.height, depth: configJson.wallSize.depth },
      door: { url: configJson.doorModel.url, fbx: configJson.doorModel.fbx },
      desk: { url: configJson.deskModel.url, fbx: configJson.deskModel.fbx},
      phone: { url: configJson.phoneModel.url, fbx: configJson.phoneModel.fbx},
      receptionist: { url: configJson.receptionistModel.url, fbx: configJson.receptionistModel.fbx},
      floorRoughness: configJson.floorRoughness,
      floorMetalness: configJson.floorMetalness,
      floorEnvMap: configJson.floorEnvMap,
    });
    loaderInstance.object.translateY(configJson.wallSize.height / 4)
    this.scene.add(loaderInstance.object);

    this.createRoomLoaders(this.rooms,loaderInstance.vectorLeftList,loaderInstance.vectorRightList);

    var floor = new Ground({ textureUrl: configJson.groundTextureUrl, size: configJson.groundSize, metalness: configJson.groundMetalness, roughness: configJson.groundRoughness })
    floor.object.translateZ(-0.01);
    this.scene.add(floor.object);

    this.ambientLight = new THREE.AmbientLight(this.ambientColor, this.ambientIntensity);
    this.scene.add(this.ambientLight);

    this.directionalLight = new THREE.DirectionalLight(this.directionalColor, this.directionalIntensity);
    this.directionalLight.castShadow = true;
    this.directionalLight.position.set(
      this.directionalLightPosition.x,
      this.directionalLightPosition.y,
      this.directionalLightPosition.z
    );
    this.directionalLight.lookAt(new THREE.Vector3(0, 0, 0))

    this.directionalLight.shadow.mapSize.width = 2048;
    this.directionalLight.shadow.mapSize.height = 2048;

    this.directionalLight.shadow.camera.left = -15;
    this.directionalLight.shadow.camera.right = 15;
    this.directionalLight.shadow.camera.top = 5;
    this.directionalLight.shadow.camera.bottom = -10;
    this.directionalLight.shadow.camera.near = -15;
    this.directionalLight.shadow.camera.far = 25;

    this.scene.add(this.directionalLight);

    //const shadowCameraHelper = new THREE.CameraHelper(directionalLight.shadow.camera);
    //this.scene.add(shadowCameraHelper);

    //const directionalLightHelper = new THREE.DirectionalLightHelper(directionalLight);
    //this.scene.add(directionalLightHelper);

    //*Camera
    let aspectRatio = this.getAspectRatio();
    this.camera = new THREE.PerspectiveCamera(this.fieldOfView, aspectRatio,
      this.nearClippingPane, this.farClippingPane);
    this.camera.position.z = this.cameraZ;
    this.camera.position.y = this.cameraY;
    //* Renderer
    // Use canvas element in template
    this.renderer = new THREE.WebGLRenderer({ canvas: this.canvas });
    this.renderer.setPixelRatio(devicePixelRatio);
    this.renderer.setSize(this.canvas.clientWidth * 3, this.canvas.clientHeight * 3);
    this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;
    this.renderer.shadowMap.enabled = true;
    //this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;

    //Camera
    this.controls = new OrbitControls(this.camera, this.renderer.domElement);
    this.controls.mouseButtons = {
      // LEFT is defined in method "onMouseDown"
      MIDDLE: THREE.MOUSE.DOLLY,
      RIGHT: THREE.MOUSE.ROTATE
    };
    this.controls.target.set(0, 0, 0);
    this.controls.maxDistance = 40;
    this.controls.minDistance = 5;
    //controls.update();
  }

  /**
  * Render the scene
  *
  * @private
  * @memberof HospitalFloorComponent
  */
  private render() {
    requestAnimationFrame(() => this.render());
    this.renderer.render(this.scene, this.camera);
  }

  updateAmbientLight(): void {
    if (this.ambientLight) {
      this.ambientLight.intensity = this.ambientIntensity;
      this.ambientLight.color.set(this.ambientColor);
    }
  }

  updateDirectionalLight(): void {
    if (this.directionalLight) {
      this.directionalLight.intensity = this.directionalIntensity;
      this.directionalLight.color.set(this.directionalColor);
      this.directionalLight.position.set(
        this.directionalLightPosition.x,
        this.directionalLightPosition.y,
        this.directionalLightPosition.z
      );
    }
  }

  toggleAmbientLightControls(): void {
    this.showAmbientLightControls = !this.showAmbientLightControls;
  }

  toggleDirectionalLightControls(): void {
    this.showDirectionalLightControls = !this.showDirectionalLightControls;
  }

  createRoomLoaders(Rooms: Room[]|null, LeftSide: THREE.Vector2[], RightSide: THREE.Vector2[]): void {
    var j=0,k=0;
    for(var i=0; i < Rooms!.length; i++){
      var x,z,left;
      if (i%2==0) {
        x = LeftSide[j].width-4.5;
        z = LeftSide[j].height+0.5;
        left = true;
        j++;
      } else {
        x = RightSide[k].width-5.5;
        z = RightSide[k].width-7.5;
        left = false;
        k++;
      }
      console.log(Rooms![i]);
      console.log("x=",x," z=",z);
      var roomLoaderInstance = new RoomLoader({
        room: Rooms![i],
        leftSide: left,
        map: configJson.roomMap,
        roomSize: configJson.roomSize,
        wallTextureUrl: configJson.wallTextureUrl,
        wallSize: { width: configJson.wallSize.width, height: configJson.wallSize.height, depth: configJson.wallSize.depth },
        table: { url: configJson.tableModel.url, obj: configJson.tableModel.obj, mtl: configJson.tableModel.mtl },
        tableWithPerson: { url: configJson.tableWithPersonModel.url, obj: configJson.tableWithPersonModel.obj, mtl: configJson.tableWithPersonModel.mtl },
        door: { url: configJson.doorModel.url, fbx: configJson.doorModel.fbx },
        bench: { url: configJson.benchModel.url, fbx: configJson.benchModel.fbx },
        cart: { url: configJson.cartModel.url, fbx: configJson.cartModel.fbx },
        oxygene: { url: configJson.oxygeneModel.url, fbx: configJson.oxygeneModel.fbx },
        doctor: { url: configJson.doctorModel.url, fbx: configJson.doctorModel.fbx }
      });
      roomLoaderInstance.object.translateY(configJson.wallSize.height / 4)
      roomLoaderInstance.object.translateX(x);
      roomLoaderInstance.object.translateZ(z-4.5);
      this.roomLoaders?.push(roomLoaderInstance);
      this.scene.add(roomLoaderInstance.object);
    }
  }

  private getCurrentDateTime(): string {
    const now = new Date();
    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0'); // Month is zero-based
    const day = String(now.getDate()).padStart(2, '0');
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');

    return `${year}-${month}-${day}T${hours}:${minutes}`;
  }

  handleDateTimeSelection(): void {
    const date = new Date(this.selectedDateTime);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const hour = String(date.getHours()).padStart(2, '0');
    const minute = String(date.getMinutes()).padStart(2, '0');
    const time = `${year}${month}${day}${hour}${minute}`;
    this.roomLoaders!.forEach(async element => {
      console.log(element);
      element.toggleTableVisibility((await this.service.isRoomOccupied(this.token, element.room.Number, time)).body);
    });
  }

  onMouseDown(event: MouseEvent): void {
    if (event.button !== 0) return; // Only continues if it's a LEFT click

    const coords = new THREE.Vector2(
      (event.clientX / this.renderer.domElement.clientWidth) * 2 - 1,
      -((event.clientY / this.renderer.domElement.clientHeight) * 2 - 1.5),
    );
  
    this.raycaster.setFromCamera(coords, this.camera);
  
    const intersections = this.raycaster.intersectObjects(this.scene.children, true);
    if (intersections.length > 0) {
      const selectedObject = intersections[0].object;
      
      if (!(selectedObject instanceof THREE.Mesh)) return;
      const parentRoomLoader = this.findParentRoomLoader(selectedObject);
      if (!parentRoomLoader) return;
      const isTable = parentRoomLoader.isTable(selectedObject);
      console.log("Is it a table? " + isTable);

      if (!isTable) return;
      //const color = new THREE.Color(Math.random(), Math.random(), Math.random()); // Test the click
      //selectedObject.material.color.set(color);
      const pos = selectedObject.getWorldPosition(new THREE.Vector3());
      console.log(`Camera Position X = ${pos.x}`); 
      console.log(parentRoomLoader.room);
      this.selectedRoom = parentRoomLoader;
      this.showRoomInfo = false;

      this.changeCameraPositionAndAngle(
        new THREE.Vector3(pos.x, this.camera.position.y, this.camera.position.z),
        new THREE.Vector3(pos.x, pos.y, pos.z),
        this.duration,
        "power2.inOut"
      );
    }
  }  
  
  findParentRoomLoader(object: THREE.Object3D): RoomLoader | null {
    if (!this.roomLoaders) return null;

    for (const roomLoader of this.roomLoaders) {
        let found = false;
        roomLoader.object.traverse((child) => {
            if (child === object) found = true;
        });

        if (found) return roomLoader;
    }
    return null;
  }

  iPressed(event: KeyboardEvent) {
    console.log(event.key);
    if(event.key === 'x') this.closeRoomInfo();
    if(!this.selectedRoom || event.key !== 'i') return;
    this.showRoomInfo = !this.showRoomInfo;
  }

  closeRoomInfo() {
    this.selectedRoom = null;
    this.showRoomInfo = false;

    this.changeCameraPositionAndAngle(
      new THREE.Vector3(this.cameraX, this.cameraY, this.cameraZ),
      new THREE.Vector3(0,0,0),
      this.duration,
      "power1.inOut"
    );
  }

  changeCameraPositionAndAngle(position: THREE.Vector3, target: THREE.Vector3, duration: number, ease: string){
    GSAP.gsap.to(this.camera.position, {x: position.x, y: position.y, z: position.z, duration: duration, ease: ease, });
    GSAP.gsap.to(this.controls.target, {x: target.x, y: target.y, z: target.z, duration: duration, });

    const lookAtTarget = new THREE.Vector3();
    lookAtTarget.copy(this.camera.getWorldDirection(new THREE.Vector3()).add(this.camera.position));
    GSAP.gsap.to(lookAtTarget, {x: target.x, y: target.y, z: target.z, duration: duration, ease: ease,
      onUpdate: () => {
        this.camera.lookAt(lookAtTarget);
      },
    });
    this.controls.update();
  }

}
