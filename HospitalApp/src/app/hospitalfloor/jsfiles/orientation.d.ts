import { Vector2 } from "three";

export default class Orientation extends Vector2 {
    constructor(h?: number, v?: number);
    
    // Horizontal orientation (alias for x)
    get h(): number;
    set h(value: number);

    // Vertical orientation (alias for y)
    get v(): number;
    set v(value: number);
}
