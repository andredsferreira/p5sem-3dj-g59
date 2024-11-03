import * as THREE from "three";

interface BoxParameters {
    wallTextureUrl: string;
    floorTextureUrl: string;
    floorWidth: number;
    floorHeight: number;
    wallHeight: number;
    wallDepth: number;
}

export default class Box {
    constructor(parameters: BoxParameters);

    group: THREE.Group;
}
