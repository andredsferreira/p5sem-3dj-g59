import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MedicalRecordService } from './medicalrecord-service';
import { AuthService } from '../auth/auth.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FamilyHistoryEntry, MedicalConditionEntry } from './entry-types';

interface FieldFamilyHistory {
  selected: boolean;
  value: string;
}

interface FieldMedicalConditionEntry {
  selected: boolean;
  value: string;
}

@Component({
  selector: 'app-medicalrecord',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './medicalrecord.component.html',
  styleUrl: './medicalrecord.component.css'
})
export class MedicalRecordComponent implements OnInit {

  record: string | null = null;
  recordDetails: any = null;
  recordNotFound = false;
  token: string | null = null;
  errorMessage: string | null = null;
  showPage = false;
  selectedFamilyEntry: FamilyHistoryEntry | null = null;
  isSearchingFamilyEntries = false;
  isCreatingFamilyEntries = false;
  isEditingFamilyEntries = false;

  selectedMedicalConditionEntry: MedicalConditionEntry | null = null;
  isSearchingMedicalConditionEntries = false;
  isCreatingMedicalConditionEntries = false;
  isEditingMedicalConditionEntries = false;

  showMessage = false;
  messageText = '';
  messageClass = '';

  activeTabIndex: number | null = null; // Índice da aba ativa
  showSearchMenus: { [key: string]: boolean } = { familyHistory: false, allergies: false };
  familyHistoryResults: FamilyHistoryEntry[] | null = [];
  medicalConditionEntryResults: MedicalConditionEntry[] | null = [];

  attributesFamilyHistory = [
    { key: 'relative', label: 'Familiar' },
    { key: 'history', label: 'Detalhes' }
  ];
  attributesMedicalConditionEntry = [
    { key: 'condition', label: 'Condição' },
    { key: 'year', label: 'Ano diagnosticado' }
  ];
  fieldsFamilyEntry: { [key: string]: FieldFamilyHistory } = {};
  fieldsMedicalConditionEntry: { [key: string]: FieldMedicalConditionEntry } = {};

  constructor(private route: ActivatedRoute, private medicalRecordService: MedicalRecordService, private authService: AuthService) {}

  ngOnInit(): void {
    this.token = localStorage.getItem('token');
  
    if (!this.token) {
      this.errorMessage = 'Você não tem as permissões necessárias para aceder esta página.';
      return;
    }
  
    const role = this.authService.getRoleFromToken(this.token).toLowerCase();
    if (role !== "admin" && role !== "doctor") {
      this.errorMessage = 'Você não tem as permissões necessárias para aceder esta página.';
      return;
    }
  
    this.record = this.route.snapshot.paramMap.get('id');
    if (!this.record) {
      this.errorMessage = 'Registo médico não foi inserido.';
      return;
    }
  
    this.validateRecord(this.record);

    this.attributesFamilyHistory.forEach((attr) => {
      this.fieldsFamilyEntry[attr.key] = { selected: false, value: '' };
    });
    this.attributesMedicalConditionEntry.forEach((attr) => {
      this.fieldsFamilyEntry[attr.key] = { selected: false, value: '' };
    });
  }
  
  private async validateRecord(id: string): Promise<void> {
    try {
      const record = await this.fetchRecord(id);
      if (record === null) {
        this.errorMessage = 'Este registo médico não existe.';
        return;
      }
      this.showPage = true;
    } catch (error) {
      this.errorMessage = 'Erro ao validar o registo médico.';
      console.error(error);
    }
  }
  
  async fetchRecord(id: string): Promise<string | null> {
    try {
      const result = await this.medicalRecordService.getMedicalRecord(this.token, id);
      return result.body || null;
    } catch (error) {
      console.error("Erro ao buscar o registo médico:", error);
      return null;
    }
  }  

  resetFields() {
    this.fieldsFamilyEntry = this.attributesFamilyHistory.reduce(
      (fields, attr) => {
        fields[attr.key] = { selected: false, value: '' };
        return fields;
      },
      {} as { [key: string]: FieldFamilyHistory }
    );
    this.fieldsMedicalConditionEntry = this.attributesMedicalConditionEntry.reduce(
      (fields, attr) => {
        fields[attr.key] = { selected: false, value: '' };
        return fields;
      },
      {} as { [key: string]: FieldMedicalConditionEntry }
    );
  }

  startCreateFamilyEntry() : void {
    this.resetFields();
    this.isCreatingFamilyEntries = true;
  }

  startCreateMedicalConditionEntry() : void {
    this.resetFields();
    this.isCreatingMedicalConditionEntries = true;
  }

  async onCreateFamilyEntry() : Promise<void> {
    const createParams: {relative:string, history:string} = {relative: '',history: ''};
    for (const [key, field] of Object.entries(this.fieldsFamilyEntry)) {
      createParams[key as keyof {relative:string, history:string}] = field.value;
    }
    try{
      const response = await this.medicalRecordService.createFamilyEntry(this.token, this.record!, createParams);
      this.showMessage = true;
      if (response) {
        this.messageText = `Entrada ${response.body?.entryNumber} criada com sucesso!`;
        this.messageClass = 'bg-green-500 text-white';
      }
      this.familyHistoryResults?.push(response.body!);
      
    } catch (error) {
      console.error("Erro ao criar entrada:", error);
      this.showMessage = true;
      this.messageText = 'Erro ao criar entrada. Tente novamente.';
      this.messageClass = 'bg-red-500 text-white';
    } finally {
      this.isCreatingFamilyEntries = false;
      this.selectedFamilyEntry = null;
      //await this.loadPatients();

      setTimeout(() => {
        this.showMessage = false;
      }, 3000);
    }
  }

  async onCreateMedicalConditionEntry() : Promise<void> {
    const createParams: {condition:string, year:string} = {condition: '',year: ''};
    for (const [key, field] of Object.entries(this.fieldsMedicalConditionEntry)) {
      createParams[key as keyof {condition:string, year:string}] = field.value;
    }
    if (createParams.year && isNaN(Number(createParams.year))) {
      console.error("O ano tem de ser um numero");
      this.showMessage = true;
      this.messageText = 'O ano tem de ser um número.';
      this.messageClass = 'bg-red-500 text-white';
      return;
    }
    try{
      const response = await this.medicalRecordService.createMedicalConditionEntry(this.token, this.record!, createParams);
      this.showMessage = true;
      if (response) {
        this.messageText = `Entrada ${response.body?.entryNumber} criada com sucesso!`;
        this.messageClass = 'bg-green-500 text-white';
      }
      this.medicalConditionEntryResults?.push(response.body!);
      
    } catch (error) {
      console.error("Erro ao criar entrada:", error);
      this.showMessage = true;
      this.messageText = 'Erro ao criar entrada. Tente novamente.';
      this.messageClass = 'bg-red-500 text-white';
    } finally {
      this.isCreatingMedicalConditionEntries = false;
      this.selectedMedicalConditionEntry = null;
      //await this.loadPatients();

      setTimeout(() => {
        this.showMessage = false;
      }, 3000);
    }
  }
  
  cancelCreateFamilyEntry(){
    this.isCreatingFamilyEntries = false;
  }

  cancelCreateMedicalConditionEntry(){
    this.isCreatingMedicalConditionEntries = false;
  }

  toggleTab(index: number): void {
    this.activeTabIndex = this.activeTabIndex === index ? null : index;
  }

  toggleSearchMenu(index: number): void {
    this.showSearchMenus[index] = !this.showSearchMenus[index];
  }

  listFamilyHistory(): void {
    this.resetFields();
    this.isSearchingFamilyEntries = true;
  }

  listMedicalConditionEntry(): void {
    this.resetFields();
    this.isSearchingMedicalConditionEntries = true;
  }

  async submitSearchFamilyEntry(): Promise<void> {
    const searchParams: {relative?:string, history?: string} = {};

    for (const [key, field] of Object.entries(this.fieldsFamilyEntry)) {
      if (field.selected) {
        searchParams[key as keyof {relative:string, history: string}] = field.value;
      }
    }

    try{
      var result = await this.medicalRecordService.getFamilyHistoryEntries(this.token, this.record!, searchParams);
      this.familyHistoryResults = result.body;
      console.log("listing successful")
    } catch (error) {
      console.log(error);
      this.familyHistoryResults = [];
      this.showMessage = true;
      this.messageText = 'Nenhuma entrada encontrada.';
      this.messageClass = 'bg-red-500 text-white';
    } finally {
      this.isSearchingFamilyEntries = false;
      setTimeout(() => {
        this.showMessage = false;
      }, 3000);
    }
  }

  async submitSearchMedicalConditionEntry(): Promise<void> {
    const searchParams: {condition?:string, year?: string} = {};

    for (const [key, field] of Object.entries(this.fieldsMedicalConditionEntry)) {
      if (field.selected) {
        searchParams[key as keyof {condition:string, year: string}] = field.value;
      }
    }

    if (searchParams.year && isNaN(Number(searchParams.year))) {
      console.error("O ano tem de ser um numero");
      this.showMessage = true;
      this.messageText = 'O ano tem de ser um número.';
      this.messageClass = 'bg-red-500 text-white';
      return;
    }

    console.log(searchParams.condition);    console.log(searchParams.year);

    try{
      var result = await this.medicalRecordService.getMedicalConditionEntries(this.token, this.record!, searchParams);
      this.medicalConditionEntryResults = result.body;
      console.log("listing successful")
    } catch (error) {
      console.log(error);
    } finally {
      this.isSearchingMedicalConditionEntries = false;
    }
  }

  cancelSearchFamilyHistory() : void {
    this.isSearchingFamilyEntries = false;
  }

  cancelSearchMedicalConditionEntry() : void {
    this.isSearchingMedicalConditionEntries = false;
  }

  onEditFamilyEntry(entry: FamilyHistoryEntry): void {
    this.resetFields();
    this.selectedFamilyEntry = { ...entry };
    this.isEditingFamilyEntries = true;
    
    this.attributesFamilyHistory.forEach((attr) => {
      const value = entry[attr.key as keyof FamilyHistoryEntry] || '';
      this.fieldsFamilyEntry[attr.key].value = value;
    });
  }

  onEditMedicalConditionEntry(entry: MedicalConditionEntry): void {
    this.resetFields();
    this.selectedMedicalConditionEntry = { ...entry };
    this.isEditingMedicalConditionEntries = true;
    
    this.attributesMedicalConditionEntry.forEach((attr) => {
      const value = entry[attr.key as keyof MedicalConditionEntry] || '';
      this.fieldsMedicalConditionEntry[attr.key].value = value;
    });
  }

  async submitEditFamilyEntry(entry: FamilyHistoryEntry | null): Promise<void> {
    const editParams: {relative?:string, history?: string} = {};

    for (const [key, field] of Object.entries(this.fieldsFamilyEntry)) {
      if (field.selected) {
        editParams[key as keyof {relative:string, history: string}] = field.value;
      }
    }

    try{
      var result = await this.medicalRecordService.editFamilyHistoryEntry(this.token, entry!.entryNumber, editParams);
      console.log("edit successful")
      const entryToChange = this.familyHistoryResults?.find(e => e.entryNumber === entry!.entryNumber);
      if (editParams.history) entryToChange!.history = editParams.history; 
      if (editParams.relative) entryToChange!.relative = editParams.relative; 
    } catch (error) {
      console.log(error);
    } finally {
      this.isEditingFamilyEntries = false;
      this.resetFields();
    }
  }

  async submitEditMedicalConditionEntry(entry: MedicalConditionEntry | null): Promise<void> {
    const editParams: {condition?:string, year?: string} = {};

    for (const [key, field] of Object.entries(this.fieldsMedicalConditionEntry)) {
      if (field.selected) {
        editParams[key as keyof {condition:string, year: string}] = field.value;
      }
    }

    if (editParams.year && isNaN(Number(editParams.year))) {
      console.error("O ano tem de ser um numero");
      this.showMessage = true;
      this.messageText = 'O ano tem de ser um número.';
      this.messageClass = 'bg-red-500 text-white';
      return;
    }

    try{
      var result = await this.medicalRecordService.editMedicalConditionEntry(this.token, entry!.entryNumber, editParams);
      console.log("edit successful")
      const entryToChange = this.medicalConditionEntryResults?.find(e => e.entryNumber === entry!.entryNumber);
      if (editParams.condition) entryToChange!.condition = editParams.condition; 
      if (editParams.year) entryToChange!.year = editParams.year; 
    } catch (error) {
      console.log(error);
    } finally {
      this.isEditingMedicalConditionEntries = false;
      this.resetFields();
    }
  }

  // Cancel edit operation
  cancelEditFamilyEntry(): void {
    this.isEditingFamilyEntries = false;
  }

  cancelEditMedicalConditionEntry(): void {
    this.isEditingMedicalConditionEntries = false;
  }
}
