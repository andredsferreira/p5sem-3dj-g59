import * as THREE from 'three';

export default class Box extends THREE.Group {
    constructor(parameters: string);
    mesh: THREE.Mesh;
}