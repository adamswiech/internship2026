import { useEffect, useState } from "react";
import Table from "./Table";

const  MainView = () =>{
    const [users, setUsers] = useState([]);
    const [offset,setOffset] = useState({offset: 0, rows: 0});
    const [imie, setImie] = useState("");
    const [nazwisko, setNazwisko] = useState("");
    const [kraj, setKraj] = useState("");
    const [filter, setFilter] = useState<{imie:string,nazwisko: string,kraj: string}>({imie: "",nazwisko: "",kraj: ""});
    const [loading, setLoading] = useState(true);
    useEffect(() => {
    const fetchUsers = async () => {
        try {
            const userData = {
                offset: offset.offset,
                imieFilter: filter.imie,
                nazwiskoFilter: filter.nazwisko,
                krajFilter: filter.kraj,
            };
            setLoading(true);
            const response = await fetch(`https://localhost:7046/User/GetUsers`,{
                method: 'POST',                   
                headers: {
                    'Content-Type': 'application/json',  
                },
                body: JSON.stringify(userData)
            });

            if (!response.ok) {
            throw new Error('Failed to fetch users');
            }

                const data = await response.json();
                setUsers(data);
                setLoading(false);
        } catch (err) {

        }};
        fetchUsers();
    }, [offset,filter]);

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const userData = {
                    offset: offset,
                    imieFilter: filter.imie,
                    nazwiskoFilter: filter.nazwisko,
                    krajFilter: filter.kraj,
                };
                const response = await fetch(`https://localhost:7046/User/GetRowCount`,{
                    method: 'POST',                   
                    headers: {
                        'Content-Type': 'application/json',  
                    },
                    body: JSON.stringify(userData)
                });

                if (!response.ok) {
                throw new Error('Failed to fetch users');
                }

                    const data = await response.json();
                    setOffset({...offset,rows: data});
            } catch (err) {}
        };
        fetchUsers();
    }, [filter]);

    //2600 + 5000
    //10000
    const changeOffset = (v : number) =>{
        if(offset.offset == 0 && v < 0) return;

        setOffset({...offset,offset: offset.offset + v});
    }

    const handleFilters = ()=>{
        const newFilter = {
            imie : imie,
            nazwisko: nazwisko,
            kraj: kraj
        };
        setOffset({...offset, offset: 0});
        setFilter(newFilter);
    };
    const formatNumber = (n : number)=>{
        n = Math.round(n);
        return n.toLocaleString('en-US').replace(/,/g, '_');
    }
    return (
        
        <div>
            <div>
                <h2>Users</h2>
                <div>
                    <button onClick={handleFilters}>filter</button>
                    <label className="filter">
                        Filtruj Imie 
                        <input type="text" value={imie} onChange={(e)=> setImie(e.target.value)} />
                    </label>
                    <label className="filter">
                        Filtruj Nazwisko 
                        <input type="text" value={nazwisko} onChange={(e)=> setNazwisko(e.target.value)} />
                    </label>
                    <label className="filter">
                        Filtruj Kraju 
                        <input type="text" value={kraj} onChange={(e)=> setKraj(e.target.value)} />
                    </label>
                </div>
                <div>
                    <Table body={users}/>
                    {(loading)&&
                    <p className="abs">loading</p>}
                </div>
                <div className="footer">
                    <button onClick={() =>changeOffset(-20)}>{'<'}</button>
                    <div>{formatNumber((offset.offset / 20) + 1)} / {formatNumber(offset.rows / 20)}</div>
                    <button onClick={() =>changeOffset(20)}>{'>'}</button>
                </div>
            </div>
        </div>
    );
}

export default MainView;