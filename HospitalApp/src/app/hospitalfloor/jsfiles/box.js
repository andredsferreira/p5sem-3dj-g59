import * as THREE from "three";
import Ground from "./ground";
import Wall from "./wall";

/*
 * parameters = {
 *  wallTextureUrl: String
 *  floorTextureUrl: String
 *  floorWidth: number
 *  floorHeight: number
 *  wallHeight: number
 *  wallDepth: number
 * }
 */

export default class Box {
    constructor(parameters) {
        
        this.parameters = parameters;

        this.group = new THREE.Group();

        var wall = new Wall({textureUrl: this.parameters.wallTextureUrl, size:{height:this.parameters.wallHeight, width:this.parameters.floorWidth, depth:this.parameters.wallDepth}});
        wall.object.translateZ(this.parameters.floorHeight/2);
        this.group.add(wall.object);

        var wallClone = wall.object.clone();
        wallClone.translateZ(-this.parameters.floorHeight);
        this.group.add(wallClone);

        wall = new Wall({textureUrl: this.parameters.wallTextureUrl, size:{height:this.parameters.wallHeight, width:this.parameters.floorHeight, depth:this.parameters.wallDepth}});
        wall.object.rotateY(Math.PI/2);
        wall.object.translateZ(this.parameters.floorWidth/2);
        this.group.add(wall.object);

        wallClone = wall.object.clone();
        wallClone.translateZ(-this.parameters.floorWidth);
        this.group.add(wallClone);

        //Hospital Floor
        var floor = new Ground({textureUrl: this.parameters.floorTextureUrl, size:{height:this.parameters.floorHeight, width:this.parameters.floorWidth}})
        floor.object.translateZ(-this.parameters.wallHeight/2);
        this.group.add(floor.object);
    }
}