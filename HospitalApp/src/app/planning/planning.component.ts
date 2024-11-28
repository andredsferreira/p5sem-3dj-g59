import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PlanningService } from './planning-service';
import { Operation, RoomSchedule, SchedulerResponse, StaffSchedules } from './schedule-type';
import { OperationTypeService } from '../operation-types/operation-type.service';

@Component({
  selector: 'app-planning',
  templateUrl: './planning.component.html',
  styleUrls: ['./planning.component.css'],
  imports: [CommonModule, FormsModule],
  standalone: true
})
export class PlanningComponent implements OnInit {
  isModalOpen = false;
  currentOperation: Operation = { staffList: [], operationType: '' };
  operations: Operation[] = [];  
  operationTypes: any[] = [];
  output: SchedulerResponse | null = null;
  parsedRoomSchedule: RoomSchedule | null = null;
  parsedStaffSchedules: StaffSchedules | null = null;

  constructor(private service : PlanningService, private ots : OperationTypeService) {}

  async ngOnInit(): Promise<void> {
    const l = await this.ots.listOperationTypes();
    l.find(
      (type: { name: any; }) => {this.operationTypes.push(type.name)}
    );
  }

  newStaffMember = '';

  openModal() {
    this.isModalOpen = true;
  }

  addStaff() {
    if (this.newStaffMember.trim()) {
      this.currentOperation.staffList.push(this.newStaffMember.trim());
      this.newStaffMember = '';
    }
  }

  addOperation() {
    if (
      this.currentOperation.operationType &&
      this.currentOperation.staffList.length > 0
    ) {
      this.operations.push({ ...this.currentOperation });
      this.currentOperation = { staffList: [], operationType: '' };
    }
  }

  async confirmSchedule() {
    const roomSchedule: [number, number, string][] = [];
    const staffSchedules = new Map<string, [number, number, string][]>();

    this.output = await this.service.scheduleSurgeries();

    console.log(`final time = ${this.output['final-time']}`)
    this.parseSchedulerResponse();
    this.isModalOpen = false;
  }
  parseSchedulerResponse() {
    if (!this.output) return;

    // Parse "room-schedule"
    this.parsedRoomSchedule = {
      values: this.output['room-schedule']
        .match(/\(([^)]+)\)/g)
        ?.map((frame) => {
          const [minute_i, minute_f, operation] = frame
            .slice(1, -1)
            .split(',');
          return {
            minute_i: parseInt(minute_i, 10),
            minute_f: parseInt(minute_f, 10),
            operation
          };
        }) || []
    };

    // Parse "staff-schedules"
    this.parsedStaffSchedules = {
      staffs: this.output['staff-schedules']
        .match(/\(([^)]+),\[(.*?)\]\)/g)
        ?.map((entry) => {
          const [staff, frames] = entry
            .slice(1, -1)
            .split(',[');

          const parsedFrames = frames
            .match(/\(([^)]+)\)/g)
            ?.map((frame) => {
              const [minute_i, minute_f, operation] = frame
                .slice(1, -1)
                .split(',');
              return {
                minute_i: parseInt(minute_i, 10),
                minute_f: parseInt(minute_f, 10),
                operation
              };
            }) || [];

          return {
            staff,
            values: parsedFrames
          };
        }) || []
    };
  }
}
