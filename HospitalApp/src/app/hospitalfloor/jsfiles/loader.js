import * as THREE from "three";
import Wall from "./wall";
import Window from "./window";
import Ground from "./ground";
import { FBXLoader } from 'three/examples/jsm/loaders/FBXLoader.js';

export default class Loader {
    constructor(description) {
        this.description = description;

        // Create a group of objects
        this.object = new THREE.Group();

        // Create the ground
        this.ground = new Ground({ textureUrl: this.description.groundTextureUrl, size: this.description.groundSize, roughness: this.description.floorRoughness, metalness: this.description.floorMetalness, envMap: this.description.floorEnvMap });
        this.ground.object.translateZ(-this.description.wallSize.height/4);
        this.object.add(this.ground.object);

        // Create a wall
        this.wall = new Wall({ textureUrl: this.description.wallTextureUrl, size: this.description.wallSize });
        //this.window = new Window({ textureUrl: this.description.wallTextureUrl, size: this.description.wallSize });

        this.vectorLeftList = [];
        this.vectorRightList = [];
        // Build the maze
        let wallObject/*, windowObject*/;

        function loadFbx(description, objectDesc, i, j, { scale, translateY=0, translateZ=0, scaleX = 0, rotateY = 0, translateX=0 }){
            const fbxLoader = new FBXLoader();
            fbxLoader.load(objectDesc.url + objectDesc.fbx, (obj) => {
                obj.position.set(
                    i - description.groundSize.width / 2 + 0.5,
                    0.5,
                    j - description.groundSize.height / 2
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

        //function loadWindow(i,j, wall, description){
        //    wallObject = wall.object.clone();
        //    wallObject.rotateOnAxis(new THREE.Vector3(0,1,0), Math.PI/2);
        //    wallObject.position.set(
        //        i - description.groundSize.width/2, 
        //        0.5, 
        //        j - description.groundSize.height/2 + 0.5);
        //    loadFbx.call(this, description, description.window, i, j, {scale: 0.0005, rotateY: 1.55, translateX: -0.6, scaleX: 1.5});
        //    //this.object.add(wallObject);
        //}

        for (let i = 0; i <= this.description.groundSize.width; i++) { // In order to represent the eastmost walls, the map width is one column greater than the actual maze width
            for (let j = 0; j <= this.description.groundSize.height; j++) { // In order to represent the southmost walls, the map height is one row greater than the actual maze height
                
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
                } else if (this.description.map[j][i] == 4) {
                    wallObject = (new Wall({ textureUrl: this.description.wallWithIconTextureUrl, size: this.description.wallSize })).object;
                    wallObject.position.set(
                        i - this.description.groundSize.width/2 + 0.5, 
                        0.5, 
                        j - this.description.groundSize.height/2);
                    this.object.add(wallObject);
                }/*else if (this.description.map[j][i] == 5) {
                    windowObject = this.window.object.clone();
                    windowObject.rotateOnAxis(new THREE.Vector3(0, 1, 0), Math.PI / 2);
                    windowObject.position.set(
                        i - this.description.groundSize.width / 2, 
                        0.5, 
                        j - this.description.groundSize.height / 2 +0.5
                    );
                    this.object.add(windowObject);
                } */else {
                    switch (this.description.map[j][i]) {
                        case 5:
                            loadFbx.call(this, this.description, this.description.receptionist, i, j, { scale: 0.006, translateY: -1, translateX: -0.6, translateZ: 0.4 }); // Receptionist
                            loadFbx.call(this, this.description, this.description.desk, i, j, {scale: 0.015, translateY: -0.95, translateX: -0.5, scaleX: -0.93}); // Desk
                            loadFbx.call(this, this.description, this.description.phone, i, j, {scale: 0.00015, translateY: -0.35, translateZ: .25, scaleX: -0.93}); // Phone
                            break;
                        case 6:
                            this.buildBanner(i,j);
                            loadFbx.call(this, this.description, this.description.door, i, j, {scale: 0.00155, translateY: -0.95, translateX: -0.5, scaleX: -0.93});
                            break;
                        case 7:
                            loadFbx.call(this, this.description, this.description.door, i, j, {scale: 0.00155, translateY: -0.95, translateX: 0.5, scaleX: 0.93});
                            break;
                        case 10:
                            console.log("Left");
                            this.vectorLeftList.push(new THREE.Vector2(i,j));
                            break;
                        case 11:
                            console.log("Right")
                            this.vectorRightList.push(new THREE.Vector2(i,j));
                            break;    
                    }
                }
            }
        }
    }
    buildBanner(i,j){
        const texture = new THREE.TextureLoader().load(this.description.bannerTextureUrl);
        texture.colorSpace = THREE.SRGBColorSpace;
        
        texture.wrapS = THREE.ClampToEdgeWrapping;

        const geometry = new THREE.BoxGeometry(this.description.wallSize.width*2, this.description.wallSize.height/2, this.description.wallSize.depth/5);
        
        const material = new THREE.MeshStandardMaterial({ map: texture });
        this.banner = new THREE.Mesh(geometry, material);
        
        this.banner.castShadow = true;
        this.banner.receiveShadow = true;

        this.banner.position.set(
            i - this.description.groundSize.width/2 + 1, 
            1.95, 
            j - this.description.groundSize.height/2);
        this.object.add(this.banner);
    }
        
}