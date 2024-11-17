import * as THREE from "three";

/*
 * parameters = {
 *  wallTextureUrl: string
 *  size: Vector3
 * }
 */

export default class Window {
    constructor(parameters) {
        this.description = parameters;
        // Create a group of objects
        this.object = new THREE.Group();
        const geometry = new THREE.BoxGeometry(
            this.description.size.width,
            this.description.size.height,
            this.description.size.depth
        );
        const material = new THREE.MeshPhysicalMaterial({  
            roughness: 0,  
            transmission: 0.8,
        });
        const mesh = new THREE.Mesh(geometry,material);
        this.object.add(mesh);
    }
}