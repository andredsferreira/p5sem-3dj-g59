import * as THREE from 'three';

interface GroundParameters {
    textureUrl: string;
    size: { width: number; height: number };
}

export default class Ground {
    constructor(parameters: GroundParameters);

    // Expose the 'object' property as a THREE.Mesh for external access
    object: THREE.Mesh;
}