export interface Operation {
    staffList: string[];
    operationType: string;
}

export interface Frame {
    minute_i: number;
    minute_f: number;
    operation: string;
  }
  
  export interface RoomSchedule {
    values: Frame[];
  }
  
  export interface StaffSchedule {
    staff: string;
    values: Frame[];
  }
  
  export interface StaffSchedules {
    staffs: StaffSchedule[];
  }
  
  export interface SchedulerResponse {
    'room-schedule': string;
    'staff-schedules': string;
    'final-time': number;
  }