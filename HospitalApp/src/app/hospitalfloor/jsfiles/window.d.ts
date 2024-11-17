import * as THREE from 'three';

interface WindowParameters {
    textureUrl: string;
    size: { width: number; height: number; depth: number };
}

export default class Wall {
    constructor(parameters: WindowParameters);

    // Expose the 'object' property as a THREE.Mesh for external access
    object: THREE.Mesh;
}