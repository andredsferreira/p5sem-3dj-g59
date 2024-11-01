import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import * as THREE from "three";
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import Ground from './jsfiles/ground';
import Box from './jsfiles/box';

@Component({
  selector: 'app-cube2',
  templateUrl: './cube2.component.html',
  styleUrl: './cube2.component.css'
})
export class Cube2Component implements AfterViewInit {
  @ViewChild('myCanvas') private canvasRef!: ElementRef;

  //* Cube Properties
  @Input() public rotationSpeedX: number = 0.05;
  @Input() public rotationSpeedY: number = 0.01;
  @Input() public size: number = 200;
  @Input() public texture: string = 'DEI_logo.gif';
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
  private geometry = new THREE.BoxGeometry(1, 1, 1);
  private material = new THREE.MeshStandardMaterial({map: this.loader.load(this.texture)});
  private cube: THREE.Mesh = new THREE.Mesh(this.geometry, this.material);
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
  * @memberof Cube2Component
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
    this.scene.add(box.group);

    this.cube.receiveShadow = true;
    this.cube.castShadow = true;
    this.scene.add(this.cube);

    const light = new THREE.AmbientLight(0xffffff, 0.5);
    this.scene.add(light);

    const spotLight = new THREE.SpotLight( 0xffffff,800);
    spotLight.position.set( 10,10,5 );
    spotLight.target = box.group;
    spotLight.penumbra = 0.4;

    this.scene.add(spotLight);

    const spotLightHelper = new THREE.SpotLightHelper(spotLight);
    this.scene.add(spotLightHelper);

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
    this.renderer.shadowMap.enabled = true;
    //this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;

    //Camera
    const controls = new OrbitControls(this.camera, this.renderer.domElement);
    controls.target.set( 0, 0, 0 );
    controls.maxDistance = 40;
    controls.minDistance = 5;
    controls.update();
  }

  /**
  * Animate the cube
  *
  * @private
  * @memberof Cube2Component
  */
  private animateCube() {
    this.cube.rotation.x += this.rotationSpeedX;
    this.cube.rotation.y += this.rotationSpeedY;
  }
  
  /**
  * Render the scene
  *
  * @private
  * @memberof Cube2Component
  */
  private render() {
    requestAnimationFrame(() => this.render());
    //this.animateCube();
    this.renderer.render(this.scene, this.camera);
  }
  
  ngAfterViewInit(): void {
    this.createScene();
    this.render();
  }
    
}
