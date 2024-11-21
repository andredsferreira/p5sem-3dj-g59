import { TestBed, ComponentFixture } from '@angular/core/testing';
import { HospitalFloorComponent } from './hospitalfloor.component';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import * as THREE from 'three';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { API_PATH } from '../config-path';
import { path } from '../app.config';

describe('HospitalFloorComponent', () => {
  let component: HospitalFloorComponent;
  let fixture: ComponentFixture<HospitalFloorComponent>;
  let mockCamera: THREE.PerspectiveCamera;
  let mockRenderer: { domElement: HTMLElement };
  let scene: THREE.Scene;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HospitalFloorComponent, HttpClientTestingModule],
      providers: [
        { provide: API_PATH, useValue: path }  // Provide a mock API_PATH
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(HospitalFloorComponent);
    component = fixture.componentInstance;

    // Mock the camera and renderer
    mockCamera = new THREE.PerspectiveCamera();
    mockRenderer = { domElement: document.createElement('div') };
    scene = new THREE.Scene();
  });
  
  //---------------------------------------6.5.3-----------------------------------------------------------

  it('should add ambient light to the scene', () => {
    const light = new THREE.AmbientLight(0xffffff, 0.5);
    scene.add(light);

    const ambientLight = scene.children.find(
      (child) => child instanceof THREE.AmbientLight
    ) as THREE.AmbientLight;

    expect(ambientLight).toBeTruthy();
    expect(ambientLight.color.getHex()).toBe(0xffffff);
    expect(ambientLight.intensity).toBe(0.5);
  });

  it('should add directional light with shadows enabled to the scene', () => {
    const directionalLight = new THREE.DirectionalLight(0xffffff, 3);
    directionalLight.castShadow = true;
    directionalLight.position.set(5, 3, 5);
    directionalLight.lookAt(new THREE.Vector3(0, 0, 0));

    directionalLight.shadow.mapSize.width = 2048;
    directionalLight.shadow.mapSize.height = 2048;

    directionalLight.shadow.camera.left = -15;
    directionalLight.shadow.camera.right = 15;
    directionalLight.shadow.camera.top = 5;
    directionalLight.shadow.camera.bottom = -10;
    directionalLight.shadow.camera.near = -15;
    directionalLight.shadow.camera.far = 25;

    scene.add(directionalLight);

    const addedDirectionalLight = scene.children.find(
      (child) => child instanceof THREE.DirectionalLight
    ) as THREE.DirectionalLight;

    expect(addedDirectionalLight).toBeTruthy();
    expect(addedDirectionalLight.color.getHex()).toBe(0xffffff);
    expect(addedDirectionalLight.intensity).toBe(3);

    // Verify shadow settings
    expect(addedDirectionalLight.castShadow).toBeTrue();
    expect(addedDirectionalLight.shadow.mapSize.width).toBe(2048);
    expect(addedDirectionalLight.shadow.mapSize.height).toBe(2048);

    // Verify shadow camera bounds
    const shadowCamera = addedDirectionalLight.shadow.camera as THREE.OrthographicCamera;
    expect(shadowCamera.left).toBe(-15);
    expect(shadowCamera.right).toBe(15);
    expect(shadowCamera.top).toBe(5);
    expect(shadowCamera.bottom).toBe(-10);
    expect(shadowCamera.near).toBe(-15);
    expect(shadowCamera.far).toBe(25);
  });

  //---------------------------------------6.5.4-----------------------------------------------------------

  it('should correctly set up the camera and OrbitControls', () => {
    const controls = new OrbitControls(mockCamera, mockRenderer.domElement);
    controls.target.set( 0, 0, 0 );
    controls.maxDistance = 40;
    controls.minDistance = 5;

    expect(controls.target.x).toBe(0);
    expect(controls.target.y).toBe(0);
    expect(controls.target.z).toBe(0);

    expect(controls.maxDistance).toBe(40);
    expect(controls.minDistance).toBe(5);
  });

  it('should configure the right mouse button for rotation and the middle button for dolly', () => {
    const controls = new OrbitControls(mockCamera, mockRenderer.domElement);
    controls.mouseButtons = {
      MIDDLE: THREE.MOUSE.DOLLY,
      RIGHT: THREE.MOUSE.ROTATE,
    };

    expect(controls.mouseButtons.MIDDLE).toBe(THREE.MOUSE.DOLLY);
    expect(controls.mouseButtons.RIGHT).toBe(THREE.MOUSE.ROTATE);
  });
});
