import * as THREE from "three";

/*
 * parameters = {
 *  textureUrl: String,
 *  size: Vector3
 * }
 */

export default class Wall {
    constructor(parameters) {
        for (const [key, value] of Object.entries(parameters)) {
            this[key] = value;
        }

        // Create a texture
        const texture = new THREE.TextureLoader().load(this.textureUrl);
        texture.colorSpace = THREE.SRGBColorSpace;
        
        texture.wrapS = THREE.RepeatWrapping;
        //texture.wrapT = THREE.ClampToEdgeWrapping;
        texture.repeat.set(this.size.width, this.size.height);
        texture.magFilter = THREE.LinearFilter;
        texture.minFilter = THREE.LinearFilter;

        // Create a ground box that receives shadows but does not cast them
        const geometry = new THREE.BoxGeometry(this.size.width, this.size.height, this.size.depth);
        
        const material = new THREE.MeshStandardMaterial({ map: texture });
        this.object = new THREE.Mesh(geometry, material);
        //this.object.rotation.x = -Math.PI / 2.0;
        
        this.object.castShadow = true;
        this.object.receiveShadow = true;
    }
}