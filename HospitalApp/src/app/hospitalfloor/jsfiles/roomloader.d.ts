import * as THREE from "three";
import Wall from "./wall";
import Ground from "./ground";

interface LoaderParameters {
    roomNumber: number;
    leftSide: boolean;
    map: number[][];
    roomSize: Vector2;
    wallTextureUrl: string;
    wallSize: Vector3;
    table: ObjMtl;
    tableWithPerson: ObjMtl;
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

export default class RoomLoader {
    roomNumber: number;
    leftSide: boolean;
    scale: THREE.Vector3;
    map?: number[][];
    size?: MazeSize;
    object: THREE.Group;
    loaded: boolean;

    constructor(parameters: LoaderParameters);
    toggleTableVisibility(occupied: boolean|null);
}
