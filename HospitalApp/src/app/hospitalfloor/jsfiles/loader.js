import * as THREE from "three";
import Wall from "./wall";
import Ground from "./ground";
import { MTLLoader } from 'three/addons/loaders/MTLLoader.js';
import { OBJLoader } from 'three/addons/loaders/OBJLoader.js';

export default class Loader {
    constructor(description) {
        this.description = description;

        // Create a group of objects
        this.object = new THREE.Group();

        // Create the ground
        this.ground = new Ground({ textureUrl: this.description.groundTextureUrl, size: this.description.groundSize });
        this.ground.object.translateZ(-this.description.wallSize.height/4);
        this.object.add(this.ground.object);

        // Create a wall
        this.wall = new Wall({ textureUrl: this.description.wallTextureUrl, size: this.description.wallSize });

        // Build the maze
        let wallObject;

        function loadObject(description, objectDesc, i, j, { scale, translateY, scaleX = 0, rotateX = 0, rotateY = 0 }) {
            const mtlLoader = new MTLLoader();
            mtlLoader.load(objectDesc.url + objectDesc.mtl, (materials) => {
                materials.preload();
        
                const objLoader = new OBJLoader();
                objLoader.setMaterials(materials);
                objLoader.load(objectDesc.url + objectDesc.obj, (obj) => {
                    obj.scale.set(scale, scale, scale);
                    obj.position.set(
                        i - description.groundSize.width / 2 + 0.5,
                        0.5,
                        j - description.groundSize.height / 2
                    );
                    obj.translateY(translateY);
        
                    if (rotateY !== 0) {
                        obj.rotateY(rotateY);
                        let rotateOthers;
                        if(rotateY > 0) rotateOthers = -0.5;
                        else rotateOthers = 0.5;
                        obj.translateZ(rotateOthers);
                        obj.translateX(rotateOthers);
                        
                    }        
                    if (scaleX !== 0) {
                        obj.scale.x *= scaleX;
                    }
                    if (rotateX !== 0) {
                        obj.rotateX(rotateX);
                        obj.translateY(-0.4);
                        obj.translateZ(-1);
                    }
        
                    obj.castShadow = true;
                    obj.receiveShadow = true;
        
                    obj.traverse((node) => {
                        if (node instanceof THREE.Mesh) {
                            node.receiveShadow = true;
                            node.castShadow = true;
                        }
                    });
        
                    this.object.add(obj); // This will now refer to the class instance
                });
            });
        }

        for (let i = 0; i <= this.description.groundSize.width; i++) { // In order to represent the eastmost walls, the map width is one column greater than the actual maze width
            for (let j = 0; j <= this.description.groundSize.height; j++) { // In order to represent the southmost walls, the map height is one row greater than the actual maze height
                /*
                    * description.map[][] | North wall | West wall  | Other Option
                    * --------------------+------------+------------|------------------
                    *          0          |     No     |     No     |      No
                    *          1          |     No     |    Yes     |      No
                    *          2          |    Yes     |     No     |      No
                    *          3          |    Yes     |    Yes     |      No
                    *          4          |     No     |     No     |    Table
                    *          5          |     No     |     No     | Table with Person
                    *          6          |     No     |     No     |  Door Left Side
                    *          7          |     No     |     No     |  Door Right Side
                    *          8          |     No     |     No     |  Door Rotated
                    */
                
                if (this.description.map[j][i] == 2 || this.description.map[j][i] == 3) {
                    wallObject = this.wall.object.clone();
                    wallObject.position.set(
                        i - this.description.groundSize.width/2 + 0.5, 
                        0.5, 
                        j - this.description.groundSize.height/2);
                    this.object.add(wallObject);
                }
                if (this.description.map[j][i] == 1 || this.description.map[j][i] == 3) {
                    wallObject = this.wall.object.clone();
                    wallObject.rotateOnAxis(new THREE.Vector3(0,1,0), Math.PI/2);
                    wallObject.position.set(
                        i - this.description.groundSize.width/2, 
                        0.5, 
                        j - this.description.groundSize.height/2 + 0.5);
                    this.object.add(wallObject);
                } else {
                    switch (this.description.map[j][i]) {
                        case 4:
                            loadObject.call(this, this.description, this.description.table, i, j, { scale: 0.2, translateY: -0.95 });
                            break;
                        case 5:
                            loadObject.call(this, this.description, this.description.tableWithPerson, i, j, { scale: 0.2, translateY: -0.95 });
                            break;
                        case 6:
                            loadObject.call(this, this.description, this.description.door, i, j, { scale: 0.105, translateY: -0.93, rotateX: Math.PI / 2 });
                            break;
                        case 7:
                            loadObject.call(this, this.description, this.description.door, i, j, { scale: 0.105, translateY: -0.93, scaleX: -1, rotateX: Math.PI / 2 });
                            break;
                        case 8:
                            loadObject.call(this, this.description, this.description.door, i, j, { scale: 0.105, translateY: -0.93, rotateX: Math.PI / 2, rotateY: Math.PI / 2 });
                            break;
                        case 9:
                            loadObject.call(this, this.description, this.description.door, i, j, { scale: 0.105, translateY: -0.93, rotateX: Math.PI / 2, rotateY: -Math.PI / 2 });
                            break;
                    }

                }
            }
        }
    }
        
}