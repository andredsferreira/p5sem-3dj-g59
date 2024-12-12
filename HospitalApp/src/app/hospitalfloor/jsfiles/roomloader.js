import * as THREE from "three";
import Wall from "./wall";
import { MTLLoader } from 'three/addons/loaders/MTLLoader.js';
import { OBJLoader } from 'three/addons/loaders/OBJLoader.js';
import { FBXLoader } from 'three/examples/jsm/loaders/FBXLoader';

export default class RoomLoader {
    constructor(description) {
        this.tableObjectNames = ["BeforeBed", "BeforeBed.001", "BeforeBed.002", "BeforeBed.003", "Support", "Base", "FinalBaseMesh"];
        this.description = description;
        this.object = new THREE.Group();
        this.wall = new Wall({ textureUrl: this.description.wallTextureUrl, size: this.description.wallSize });
        this.room = this.description.room;

        let wallObject;

        function loadFbx(description, objectDesc, i, j, { scale, translateY=0, translateZ=0, translateX=0, scaleX = 0, rotateY = 0}){
            const fbxLoader = new FBXLoader();
            fbxLoader.load(objectDesc.url + objectDesc.fbx, (obj) => {
                obj.position.set(
                    i - description.roomSize.width / 2 + 1,
                    0.5,
                    j - description.roomSize.height / 2 + 0.5
                );
                obj.scale.set(scale,scale,scale);
                if (scaleX !== 0)
                    obj.scale.x *= scaleX;

                if (rotateY !== 0) {
                    obj.rotateY(rotateY);
                    let rotateOthers;
                    if(rotateY > 0) rotateOthers = 0.1;
                    else rotateOthers = 1.1;
                    obj.translateZ(rotateOthers-0.6);
                    obj.translateX(rotateOthers);
                }  

                obj.translateZ(translateZ);
                obj.translateY(translateY);
                obj.translateX(translateX);
                
                obj.receiveShadow=true;
                obj.castShadow=true;
                obj.traverse((node) => {
                    if (node instanceof THREE.Mesh) {
                        node.receiveShadow = true;
                        node.castShadow = true;
                    }
                });
                this.object.add(obj);
            });
        }

        // Função para carregar OBJ
        function loadObj(description, objectDesc, i, j, { scale, translateY, scaleX = 0 }) {
            return new Promise((resolve, reject) => {
                const mtlLoader = new MTLLoader();
                mtlLoader.load(objectDesc.url + objectDesc.mtl, (materials) => {
                    materials.preload();
                    const objLoader = new OBJLoader();
                    objLoader.setMaterials(materials);
                    objLoader.load(objectDesc.url + objectDesc.obj, (obj) => {
                        obj.scale.set(scale, scale, scale);
                        obj.position.set(
                            i - description.roomSize.width / 2,
                            translateY + 0.5,
                            j - description.roomSize.height / 2 + 1
                        );
                        if (scaleX !== 0) obj.scale.x *= scaleX;
                        obj.castShadow = true;
                        obj.receiveShadow = true;
                        obj.traverse((node) => {
                            if (node instanceof THREE.Mesh) {
                                node.receiveShadow = true;
                                node.castShadow = true;
                            }
                        });
                        resolve(obj); // Resolva com o objeto carregado
                    }, undefined, reject);
                });
            });
        }

        // Adicionando as funcionalidades de carregar os objetos no mapa
        for (let i = 0; i <= this.description.roomSize.width; i++) {
            for (let j = 0; j <= this.description.roomSize.height; j++) {

                if (this.description.map[j][i] == 2 || this.description.map[j][i] == 3) {
                    wallObject = this.wall.object.clone();
                    wallObject.position.set(
                        i - this.description.roomSize.width/2, 
                        0.5, 
                        j - this.description.roomSize.height/2 + 0.5
                    );
                    this.object.add(wallObject);
                } if (this.description.map[j][i] == 1 || this.description.map[j][i] == 3) {
                    wallObject = this.wall.object.clone();
                    wallObject.rotateOnAxis(new THREE.Vector3(0,1,0), Math.PI/2);
                    wallObject.position.set(
                        i - this.description.roomSize.width/2+0.5, 
                        0.5, 
                        j - this.description.roomSize.height/2+1);
                    this.object.add(wallObject);
                } else {
                    switch (this.description.map[j][i]) {
                        case 4: 
                            const tablePromise = loadObj.call(this, this.description, this.description.table, i, j, { scale: 0.2, translateY: -0.95 });
                            const tableWithPersonPromise = loadObj.call(this, this.description, this.description.tableWithPerson, i, j, { scale: 0.2, translateY: -0.95 });

                            Promise.all([tablePromise, tableWithPersonPromise]).then(([tableObject, tableWithPersonObject]) => {
                                tableWithPersonObject.visible = false;

                                this.object.add(tableObject);
                                this.object.add(tableWithPersonObject);

                                tableObject.tableWithPersonObject = tableWithPersonObject;
                                this.table = tableWithPersonObject;
                            }).catch((error) => {
                                console.error("Erro ao carregar os objetos: ", error);
                            });
                            break;
                        case 5:
                            loadFbx.call(this, this.description, this.description.bench, i, j, {scale: 0.00155, translateY: -0.8, rotateY: Math.PI / 2, translateX: -0.8, translateZ: -0.8, scaleX: -0.93});
                            break;    
                        case 6:
                            loadFbx.call(this, this.description, this.description.cart, i, j, {scale: 0.0155, translateY: -1, translateX: -0.6, translateZ: 0.5});
                            break;  
                        case 7:
                            loadFbx.call(this, this.description, this.description.oxygene, i, j, {scale: 0.00055, translateY: 0.2, rotateY: Math.PI, translateX: 1, translateZ: 0.5});
                            break;
                        case 8:
                            loadFbx.call(this, this.description, this.description.doctor, i, j, { scale: 0.6, translateY: -1, translateX: -0.6, translateZ: 0.4, rotateY: Math.PI / 2 });
                            break;
                        case 9:
                            loadFbx.call(this, this.description, this.description.door, i, j, { scale: 0.00155, translateY: -0.95, rotateY: -Math.PI / 2 });
                            break;
                    }
                }
            }
        }
        if(!this.description.leftSide){ 
            const scale = new THREE.Vector3(1, 1, 1);
            scale.x *= -1;
            this.object.scale.multiply(scale);
        }
    }

    toggleTableVisibility(isOccupied) {
        console.log("Is room " + this.description.room.Number + " occupied? " + isOccupied);
        this.object.traverse((child) => {
            if (child.tableWithPersonObject) {
                child.tableWithPersonObject.visible = isOccupied;
            }
            if (child.name === 'table') {
                child.visible = !isOccupied;
            }
        });
    }

    isTable(object) {
        const isMatch = this.tableObjectNames.some(element => {
            return object.name === element;
        });
        return isMatch;
    }    
}

