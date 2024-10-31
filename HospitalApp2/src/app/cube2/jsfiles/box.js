import * as THREE from "three";

/*
 * parameters = {
 *  textureUrl: String
 * }
 */

/**
 * @param {string} parameters
 */
export default class Box extends THREE.Group{
    
    constructor(parameters) {
        super();
        for (const [key, value] of Object.entries(parameters)) {
            this[key] = value;
        }

        const texture = new THREE.TextureLoader().load(this.textureUrl);
        texture.colorSpace = THREE.SRGBColorSpace;

        texture.magFilter = THREE.LinearFilter;
        texture.minFilter = THREE.LinearMipmapLinearFilter;

        var material1 = new THREE.MeshBasicMaterial( { map: texture, side: THREE.DoubleSide } );
        var material2 = new THREE.MeshBasicMaterial( { map: texture, side: THREE.DoubleSide } );
        var material3 = new THREE.MeshBasicMaterial( { map: texture, side: THREE.DoubleSide } );

        var materialTransparent =  new THREE.MeshBasicMaterial( { transparent: true, opacity: 0, wireframe: true, side: THREE.DoubleSide} );
        var geometry = new THREE.BufferGeometry( 1, 1.5, 1 );

        var materials = [ material1, materialTransparent, material2, material2, material3, material3 ]

        var mesh = new THREE.Mesh( geometry, materials );
        mesh.castShadow = true;
        mesh.receiveShadow = true;
        this.add(this.mesh);
    }
}