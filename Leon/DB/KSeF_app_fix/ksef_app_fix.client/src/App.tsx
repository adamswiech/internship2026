import { useState, useEffect } from 'react';
import InvoiceTable from './components/invoiceTable';
import UploadInvoice from './components/uploadInvoice';
import { InvoiceCon } from './api/Invoice';
import type { Invoice } from './interfaces/Invoice';
import './App.css';

function App() {
    const [Invoices, setInvoices] = useState<Invoice[]>([]);

    useEffect(() => {
        InvoiceCon.apiInvoiceGetAllInvoices().then((inv) => {
            setInvoices(inv);
        });
    }, []);

    const refreshInvoices = async () => {
        const inv = await InvoiceCon.apiInvoiceGetAllInvoices();
        setInvoices(inv);
    };


    return (
        <>
        <UploadInvoice onUploadRefresh={refreshInvoices}/>
        <InvoiceTable items={Invoices}></InvoiceTable>
        </>
    );
}

export default App;