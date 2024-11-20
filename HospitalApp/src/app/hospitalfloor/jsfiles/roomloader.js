import * as THREE from "three";
import Wall from "./wall";
import Window from "./window";
import { MTLLoader } from 'three/addons/loaders/MTLLoader.js';
import { OBJLoader } from 'three/addons/loaders/OBJLoader.js';
import { FBXLoader } from 'three/examples/jsm/loaders/FBXLoader';

export default class RoomLoader {
    constructor(description) {
        this.description = description;
        this.object = new THREE.Group();
        this.wall = new Wall({ textureUrl: this.description.wallTextureUrl, size: this.description.wallSize });
        this.window = new Window({ textureUrl: this.description.wallTextureUrl, size: this.description.wallSize });
        this.roomNumber = this.description.roomNumber;

        let wallObject, windowObject;

        function loadFbx(description, objectDesc, i, j, { scale, translateY=0, scaleX = 0, rotateY = 0}){
            const fbxLoader = new FBXLoader();
            fbxLoader.load(objectDesc.url + objectDesc.fbx, (obj) => {
                obj.position.set(
                    i - description.roomSize.width / 2 + 1,
                    translateY + 0.5,
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
                } else if (this.description.map[j][i] == 10) {
                    windowObject = this.window.object.clone();
                    windowObject.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);
                    windowObject.position.set(
                        i - this.description.roomSize.width / 2 + 0.5, 
                        0.5, 
                        j - this.description.roomSize.height / 2 + 1
                    );
                    this.object.add(windowObject);
                } else {
                    switch (this.description.map[j][i]) {
                        case 4: // Caso 4 (mesas visíveis)
                            // Carregar ambos os objetos table e tablewithperson como promessas
                            const tablePromise = loadObj.call(this, this.description, this.description.table, i, j, { scale: 0.2, translateY: -0.95 });
                            const tableWithPersonPromise = loadObj.call(this, this.description, this.description.tableWithPerson, i, j, { scale: 0.2, translateY: -0.95 });

                            // Após o carregamento de ambos os objetos, vamos manipulá-los
                            Promise.all([tablePromise, tableWithPersonPromise]).then(([tableObject, tableWithPersonObject]) => {
                                // Tornar a mesa com pessoa invisível por padrão
                                tableWithPersonObject.visible = false;

                                // Adicionar ambos ao objeto de sala
                                this.object.add(tableObject);
                                this.object.add(tableWithPersonObject);

                                // Guardar as referências para futura manipulação de visibilidade
                                tableObject.tableWithPersonObject = tableWithPersonObject;
                            }).catch((error) => {
                                console.error("Erro ao carregar os objetos: ", error);
                            });
                            break;
                        case 5:
                            // Caso 5, similar ao caso 4
                            break;
                        case 8:
                            loadFbx.call(this, this.description, this.description.door, i, j, { scale: 0.00155, translateY: -0.95, rotateY: Math.PI / 2 });
                            break;
                        case 9:
                            loadFbx.call(this, this.description, this.description.door, i, j, { scale: 0.00155, translateY: -0.95, rotateY: -Math.PI / 2 });
                            break;
                    }
                }
            }
        }
    }

    toggleTableVisibility(isOccupied) {
        if(!isOccupied) return;
        console.log(isOccupied);
        this.object.traverse((child) => {
            if (child.tableWithPersonObject) {
                child.tableWithPersonObject.visible = isOccupied;
            }
            if (child.name === 'table') {
                child.visible = !isOccupied;
            }
        });
    }
}

