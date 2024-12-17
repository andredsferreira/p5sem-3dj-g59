import { IMedicalRecordPersistence } from '../../dataschema/IMedicalRecordPersistence';
import mongoose from 'mongoose';

const MedRecordSchema = new mongoose.Schema(
  {
    domainId: { type: String, unique: true },
    medicalRecordNumber: { type: String, unique: true},
  },
  {
    timestamps: true
  }
);

export default mongoose.model<IMedicalRecordPersistence & mongoose.Document>('MedicalRecord', MedRecordSchema);
