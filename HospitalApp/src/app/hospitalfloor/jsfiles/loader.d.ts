import * as THREE from "three";
import Wall from "./wall";
import Ground from "./ground";

interface LoaderParameters {
    map: number[][];
    groundTextureUrl: string;
    groundSize: Vector2;
    wallTextureUrl: string;
    wallSize: Vector3;
    windowSize: Vector3;
    table: ObjMtl;
    tableWithPerson: ObjMtl;
    door: Fbx;
}

interface ObjMtl {
    url: string,
    obj: string,
    mtl: string
}

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
    map?: number[][];
    size?: MazeSize;
    object: THREE.Group;
    loaded: boolean;

    constructor(parameters: LoaderParameters);
}
