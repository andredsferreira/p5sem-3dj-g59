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

        var wall1 = new Wall({textureUrl: this.parameters.wallTextureUrl, size:{height:this.parameters.wallHeight, width:this.parameters.floorWidth, depth:this.parameters.wallDepth}});
        wall1.object.translateZ(this.parameters.floorHeight/2);
        this.group.add(wall1.object);

        var wall2 = new Wall({textureUrl: this.parameters.wallTextureUrl, size:{height:this.parameters.wallHeight, width:this.parameters.floorWidth, depth:this.parameters.wallDepth}});
        wall2.object.translateZ(-this.parameters.floorHeight/2);
        this.group.add(wall2.object);

        var wall3 = new Wall({textureUrl: this.parameters.wallTextureUrl, size:{height:this.parameters.wallHeight, width:this.parameters.floorHeight, depth:this.parameters.wallDepth}});
        wall3.object.rotateY(Math.PI/2);
        wall3.object.translateZ(this.parameters.floorWidth/2);
        this.group.add(wall3.object);

        var wall4 = new Wall({textureUrl: this.parameters.wallTextureUrl, size:{height:this.parameters.wallHeight, width:this.parameters.floorHeight, depth:this.parameters.wallDepth}});
        wall4.object.rotateY(Math.PI/2);
        wall4.object.translateZ(-this.parameters.floorWidth/2);
        this.group.add(wall4.object);

        //Hospital Floor
        var floor = new Ground({textureUrl: this.parameters.floorTextureUrl, size:{height:this.parameters.floorHeight, width:this.parameters.floorWidth}})
        floor.object.translateZ(-this.parameters.wallHeight/2);
        this.group.add(floor.object);
    }
}