import * as THREE from "three";
import Wall from "./wall";
import Ground from "./ground";

interface LoaderParameters {
    map: number[][];
    groundTextureUrl: string;
    groundSize: Vector2;
    wallTextureUrl: string;
    wallWithIconTextureUrl: string;
    bannerTextureUrl: string;
    wallSize: Vector3;
    floorMetalness: number,
    floorRoughness: number,
    floorEnvMap: string,
    //windowSize: Vector3;
    //table: ObjMtl;
    //tableWithPerson: ObjMtl;
    door: Fbx;
    desk: Fbx;
    phone: Fbx;
    receptionist: Fbx;
}

/*interface ObjMtl {
    url: string,
    obj: string,
    mtl: string
}*/

interface Fbx {
    url: string,
    fbx: string
}

interface Vector2 {
    width: number;
    height: number;
}

interface Vector3 {
    width: number;
    height: number;
    depth: number;
}

export default class Loader {
    scale: THREE.Vector3;
    object: THREE.Group;
    loaded: boolean;
    vectorLeftList: THREE.Vector2[];
    vectorRightList: THREE.Vector2[];

    constructor(parameters: LoaderParameters);
}
