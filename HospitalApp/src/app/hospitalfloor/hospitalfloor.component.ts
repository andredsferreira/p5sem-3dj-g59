import { AfterViewInit, Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import * as THREE from "three";
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import Ground from './jsfiles/ground';
import configJson from './config.json';
import Loader from './jsfiles/loader';
import RoomLoader from './jsfiles/roomloader';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HospitalFloorService } from './hospitalfloor-service';
import { RoomType } from './room';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],   
  selector: 'app-hospitalfloor',
  templateUrl: './hospitalfloor.component.html',
  styleUrl: './hospitalfloor.component.css'
})
export class HospitalFloorComponent implements OnInit {

  token!: string|null
  role: string = ""
  canRenderCanvas: boolean = false

  showAmbientLightControls: boolean = false;
  showDirectionalLightControls: boolean = false;
  ambientIntensity: number = 0.5;
  ambientColor: string = "#ffffff";
  directionalIntensity: number = 3;
  directionalColor: string = "#ffffff";
  directionalLightPosition = { x: 5, y: 3, z: 5 };

  selectedDateTime: string = '';

  roomLoaders!: RoomLoader[]|null;

  rooms!: RoomType[]|null;

  private ambientLight!: THREE.AmbientLight;
  private directionalLight!: THREE.DirectionalLight;

  constructor(private auth: AuthService, private service: HospitalFloorService) { }

  async ngOnInit(): Promise<void> {
    this.token = localStorage.getItem('token');
    if (this.token) {
      this.role = this.auth.getRoleFromToken(this.token);
      this.canRenderCanvas = this.role !== 'Patient' && this.role !== "";
  
      if (this.canRenderCanvas) {
        try {
          const response = await this.service.getRooms(this.token);
          this.rooms = response.body;
          this.roomLoaders = [];
  
          // Cria a cena assim que os dados estão prontos
          this.createScene();
          this.render();
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
    });
    loaderInstance.object.translateY(configJson.wallSize.height / 4)
    this.scene.add(loaderInstance.object);

    this.createRoomLoaders(this.rooms,loaderInstance.vectorLeftList,loaderInstance.vectorRightList);

    var floor = new Ground({ textureUrl: configJson.groundTextureUrl, size: configJson.groundSize })
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
    const controls = new OrbitControls(this.camera, this.renderer.domElement);
    controls.mouseButtons = {
      //LEFT will be defined in the next sprint
      MIDDLE: THREE.MOUSE.DOLLY,
      RIGHT: THREE.MOUSE.ROTATE
    };
    controls.target.set(0, 0, 0);
    controls.maxDistance = 40;
    controls.minDistance = 5;
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

  // Methods to toggle visibility of control sections
  toggleAmbientLightControls(): void {
    this.showAmbientLightControls = !this.showAmbientLightControls;
  }

  toggleDirectionalLightControls(): void {
    this.showDirectionalLightControls = !this.showDirectionalLightControls;
  }

  createRoomLoaders(RoomNumbers: RoomType[]|null, LeftSide: THREE.Vector2[], RightSide: THREE.Vector2[]): void {
    var j=0,k=0;
    console.log(RoomNumbers);
    for(var i=0; i < RoomNumbers!.length; i++){
      var x,z,left;
      if (i%2==0) {
        x = LeftSide[j].width;
        z = LeftSide[j].height;
        left = true;
        j++;
      } else {
        x = RightSide[k].width;
        z = RightSide[k].width;
        left = false;
        k++;
      }
      var roomLoaderInstance = new RoomLoader({
        roomNumber: RoomNumbers![i].Number,
        leftSide: left,
        map: configJson.roomMap,
        roomSize: configJson.roomSize,
        wallTextureUrl: configJson.wallTextureUrl,
        wallSize: { width: configJson.wallSize.width, height: configJson.wallSize.height, depth: configJson.wallSize.depth },
        table: { url: configJson.tableModel.url, obj: configJson.tableModel.obj, mtl: configJson.tableModel.mtl },
        tableWithPerson: { url: configJson.tableWithPersonModel.url, obj: configJson.tableWithPersonModel.obj, mtl: configJson.tableWithPersonModel.mtl },
        door: { url: configJson.doorModel.url, fbx: configJson.doorModel.fbx },
        windowSize: configJson.windowSize
      });
      roomLoaderInstance.object.translateY(configJson.wallSize.height / 4)
      roomLoaderInstance.object.translateX(x-3.5);
      roomLoaderInstance.object.translateZ(z-9.5);
      this.roomLoaders?.push(roomLoaderInstance);
      this.scene.add(roomLoaderInstance.object);
    }
  }

  handleDateTimeSelection(): void {
    console.log("Selected Date/Time:", this.selectedDateTime);
    
    // You can now use this.selectedDateTime to do whatever you need, e.g., sending it to a server or processing it further
  }

}
