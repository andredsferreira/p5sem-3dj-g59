import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import * as THREE from "three";
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import Ground from './jsfiles/ground';
import configJson from './config.json';
import Loader from './jsfiles/loader';

@Component({
  selector: 'app-hospitalfloor',
  templateUrl: './hospitalfloor.component.html',
  styleUrl: './hospitalfloor.component.css'
})
export class HospitalFloorComponent implements AfterViewInit {
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

    const axesHelpers = new THREE.AxesHelper(10);
    this.scene.add(axesHelpers);

    // Create an instance of Loader
    var loaderInstance = new Loader({map: configJson.map, groundTextureUrl: configJson.floorTextureUrl, 
      groundSize: {width: configJson.floorSize.width, height: configJson.floorSize.height},
      wallTextureUrl: configJson.wallTextureUrl,
      wallSize: {width: configJson.wallSize.width, height: configJson.wallSize.height, depth: configJson.wallSize.depth},
      table: {url: configJson.tableModel.url, obj: configJson.tableModel.obj, mtl: configJson.tableModel.mtl},
      tableWithPerson: {url: configJson.tableWithPersonModel.url, obj: configJson.tableWithPersonModel.obj, mtl: configJson.tableWithPersonModel.mtl},
      door: {url: configJson.doorModel.url, obj: configJson.doorModel.obj, mtl: configJson.doorModel.mtl},
    });
    loaderInstance.object.translateY(configJson.wallSize.height/4)
    this.scene.add(loaderInstance.object);

    //Ground around hospital
    var floor = new Ground({textureUrl: configJson.groundTextureUrl, size: configJson.groundSize})
    floor.object.translateZ(-0.01);
    this.scene.add(floor.object);

    //this.scene.add(box.group);

    const light = new THREE.AmbientLight(0xffffff, 0.5);
    this.scene.add(light);

    const directionalLight = new THREE.DirectionalLight( 0xffffff,7);
    directionalLight.castShadow = true;
    directionalLight.position.set( 5,3,5 );
    directionalLight.lookAt(new THREE.Vector3(0,0,0));

    directionalLight.shadow.mapSize.width = 2048;
    directionalLight.shadow.mapSize.height = 2048;

    directionalLight.shadow.camera.left = -15;
    directionalLight.shadow.camera.right = 15;
    directionalLight.shadow.camera.top = 5;
    directionalLight.shadow.camera.bottom = -10;
    directionalLight.shadow.camera.near = -15;
    directionalLight.shadow.camera.far = 25;

    this.scene.add(directionalLight);

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
    this.renderer = new THREE.WebGLRenderer({canvas: this.canvas});
    this.renderer.setPixelRatio(devicePixelRatio);
    this.renderer.setSize(this.canvas.clientWidth*3, this.canvas.clientHeight*3);
    this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;
    this.renderer.shadowMap.enabled = true;
    //this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;

    //Camera
    const controls = new OrbitControls(this.camera, this.renderer.domElement);
    controls.mouseButtons = {
      //LEFT will be defined in the next sprint
      MIDDLE: THREE.MOUSE.DOLLY,
      LEFT: THREE.MOUSE.ROTATE
    };
    controls.target.set( 0, 0, 0 );
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
  
  ngAfterViewInit(): void {
    this.createScene();
    this.render();
  }
    
}
