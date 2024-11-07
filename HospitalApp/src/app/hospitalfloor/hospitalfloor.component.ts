import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import * as THREE from "three";
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import Ground from './jsfiles/ground';
import Box from './jsfiles/box';
import { MTLLoader } from 'three/addons/loaders/MTLLoader.js';
import { OBJLoader } from 'three/addons/loaders/OBJLoader.js';

@Component({
  selector: 'app-hospitalfloor',
  templateUrl: './hospitalfloor.component.html',
  styleUrl: './hospitalfloor.component.css'
})
export class HospitalFloorComponent implements AfterViewInit {
  @ViewChild('myCanvas') private canvasRef!: ElementRef;

  //* Stage Properties
  @Input() public cameraZ: number = 10;
  @Input() public fieldOfView: number = 30;
  @Input('nearClipping') public nearClippingPane: number = 1;
  @Input('farClipping') public farClippingPane: number = 100;

  //? Helper Properties (Private properties);
  private get canvas(): HTMLCanvasElement {
    return this.canvasRef.nativeElement;
  }
  private loader = new THREE.TextureLoader();
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

    //Ground around hospital
    var floor = new Ground({textureUrl: "ground.jfif", size:{height:40, width:40}})
    floor.object.translateZ(-0.502);
    this.scene.add(floor.object);

    var box = new Box({
      floorTextureUrl: "floor.png", 
      wallTextureUrl: "wall.jpg", 
      floorHeight:20, 
      floorWidth:10,
      wallDepth:0.1, 
      wallHeight:1
    });
    

    var mtlLoader = new MTLLoader();
    mtlLoader.load("models/SurgeryTableWithPerson/SurgeryTableWithPerson.mtl", (materials) =>{
      materials.preload();
      
      var objLoader = new OBJLoader();
      objLoader.setMaterials(materials);
      objLoader.load("models/SurgeryTableWithPerson/SurgeryTableWithPerson.obj", (object) =>{
        object.scale.set(0.2,0.2,0.2);
        object.translateY(-0.45);
        object.castShadow = true;
        object.receiveShadow = true;
        object.traverse( function( node ) { if ( node instanceof THREE.Mesh ) { node.castShadow = true; } } );
        box.group.add(object);
      })
    });

    console.log(JSON.stringify(box.group.toJSON()));
    this.scene.add(box.group);

    const light = new THREE.AmbientLight(0xffffff, 0.5);
    this.scene.add(light);

    const directionalLight = new THREE.DirectionalLight( 0xffffff,7);
    directionalLight.castShadow = true;
    directionalLight.position.set( 5,3,5 );
    directionalLight.target = box.group;

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
      RIGHT: THREE.MOUSE.ROTATE
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
