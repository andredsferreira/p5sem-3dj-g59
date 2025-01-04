export interface Occupied {
    RoomNumber: number,
    Begin?: Date,
    End?: Date,
    Status: number,
    PatientName?: string,
    PatientMRN?: string,
}
export interface Room {
    Number: number,
}