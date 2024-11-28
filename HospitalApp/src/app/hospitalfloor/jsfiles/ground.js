import * as THREE from "three";

/*
 * parameters = {
 *  textureUrl: String,
 *  size: Vector2
 *  roughness: Number,
 *  metalness: Number,
 *  envmap: ???
 * }
 */

export default class Ground {
    constructor(parameters) {
        this.description = parameters;

        const texture = new THREE.TextureLoader().load(this.description.textureUrl);
        texture.colorSpace = THREE.SRGBColorSpace;
        
        texture.wrapS = THREE.RepeatWrapping;
        texture.wrapT = THREE.RepeatWrapping;
        texture.repeat.set(this.description.size.width, this.description.size.height);
        texture.magFilter = THREE.LinearFilter;
        texture.minFilter = THREE.LinearMipmapLinearFilter;

        // Create a ground box that receives shadows but does not cast them
        const geometry = new THREE.PlaneGeometry(this.description.size.width, this.description.size.height);
        
        const materialOptions = {
            map: texture,
            side: THREE.DoubleSide,
            //roughness: this.description.roughness,
            //metalness: this.description.metalness,
        };

        if (this.description.envMap) {
            const envMap = new THREE.TextureLoader().load(this.description.envMap);
            envMap.mapping = THREE.EquirectangularReflectionMapping;
            envMap.colorSpace = THREE.SRGBColorSpace;
            materialOptions.envMap = envMap;
            materialOptions.envMapIntensity = 0.1;
            materialOptions.emissive = 0.2;
            //?
        }

        const material = new THREE.MeshStandardMaterial(materialOptions);

        this.object = new THREE.Mesh(geometry, material);
        this.object.rotation.x = -Math.PI / 2.0;
        
        this.object.castShadow = false;
        this.object.receiveShadow = true;
    }
}