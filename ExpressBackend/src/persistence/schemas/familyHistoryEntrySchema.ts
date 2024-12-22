import { IFamilyHistoryEntryPersistence } from '../../dataschema/IFamilyHistoryEntryPersistence';
import mongoose from 'mongoose';

const FamilyHistoryEntrySchema = new mongoose.Schema(
    {
        domainId: { type: String, unique: true },
        entryNumber: { type: String, required: true, unique: true},
        medicalRecordNumber: { type: String, required: true},
        relative: { type: String, required: true },
        history: { type: String, required: true },
    },
    {
        timestamps: true
    }
);

export default mongoose.model<IFamilyHistoryEntryPersistence & mongoose.Document>('FamilyHistoryEntry', FamilyHistoryEntrySchema);
