import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import * as THREE from "three";
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import Ground from './jsfiles/ground';
import Wall from './jsfiles/wall';

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
  @Input() public texture: string = 'incoerente.jpg';
  //* Stage Properties
  @Input() public cameraZ: number = 10;
  @Input() public fieldOfView: number = 30;
  @Input('nearClipping') public nearClippingPane: number = 1;
  @Input('farClipping') public farClippingPane: number = 1000;

  //? Helper Properties (Private properties);
  private get canvas(): HTMLCanvasElement {
    return this.canvasRef.nativeElement;
  }
  private loader = new THREE.TextureLoader();
  private geometry = new THREE.BoxGeometry(1, 1, 1);
  private material = new THREE.MeshBasicMaterial({map:
  this.loader.load(this.texture)});
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

    //Hospital Floor
    var floor = new Ground({textureUrl: "floor.png", size:{height:20, width:10}})
    floor.object.translateZ(-0.501);
    this.scene.add(floor.object);

    var wall1 = new Wall({textureUrl: "wall.jpg", size:{height:1, width:10, depth:0.1}});
    wall1.object.translateZ(10);
    this.scene.add(wall1.object);

    var wall2 = new Wall({textureUrl: "wall.jpg", size:{height:1, width:10, depth:0.1}});
    wall2.object.translateZ(-10);
    this.scene.add(wall2.object);

    var wall3 = new Wall({textureUrl: "wall.jpg", size:{height:1, width:20, depth:0.1}});
    wall3.object.rotateY(Math.PI/2);
    wall3.object.translateZ(5);
    this.scene.add(wall3.object);
    
    var wall4 = new Wall({textureUrl: "wall.jpg", size:{height:1, width:20, depth:0.1}});
    wall4.object.rotateY(Math.PI/2);
    wall4.object.translateZ(-5);
    this.scene.add(wall4.object);

    this.cube.receiveShadow = true;
    this.cube.castShadow = true;
    this.scene.add(this.cube);

    //const spotLight = new THREE.SpotLight( 0xffffff );
    //spotLight.position.set( 10,10,10 );

    //spotLight.castShadow = true;
    //spotLight.target = this.cube;

    //this.scene.add(spotLight);

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
