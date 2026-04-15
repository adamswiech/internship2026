import { useEffect, useState } from 'react';

type Osoba = {
    id: number;
    firstname: string;
    lastname: string;
    city: string;
    email: string;
};

type ApiResponse = {
    items: Osoba[];
    totalCount?: number;
    page?: number;
};

function isPagedResponse(value: unknown): value is ApiResponse {
    return !!value && typeof value === 'object' && Array.isArray((value as ApiResponse).items);
}

export default function ItemsTable() {
    const [rows, setRows] = useState<Osoba[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const [page, setPage] = useState(1);
    const [pageSize, setPageSize] = useState(100);

    const [search, setSearch] = useState('');
    const [searchInput, setSearchInput] = useState('');

    useEffect(() => {
        const timeout = setTimeout(() => {
            setSearch(searchInput.trim());
            setPage(1);
        }, 400);

        return () => clearTimeout(timeout);
    }, [searchInput]);

    useEffect(() => {
        const controller = new AbortController();

        async function loadData() {
            try {
                setLoading(true);
                setError(null);

                const params = new URLSearchParams({
                    page: String(page),
                    pageSize: String(pageSize),
                    search,
                });

                const response = await fetch(`/api/osoba?${params.toString()}`, {
                    signal: controller.signal,
                });

                if (!response.ok) {
                    throw new Error('Błąd pobierania danych');
                }

                const data: unknown = await response.json();

                if (Array.isArray(data)) {
                    setRows(data as Osoba[]);
                    return;
                }

                if (isPagedResponse(data)) {
                    setRows(data.items);
                    return;
                }

                throw new Error('Nieprawidłowy format odpowiedzi API');
            } catch (err: unknown) {
                if ((err as { name?: string }).name !== 'AbortError') {
                    setError(err instanceof Error ? err.message : 'Nieznany błąd');
                }
            } finally {
                setLoading(false);
            }
        }

        void loadData();

        return () => controller.abort();
    }, [page, pageSize, search]);

    const hasNextPage = rows.length === pageSize;

    return (
        <div className="table-page">
            <div className="toolbar">
                <input
                    type="text"
                    placeholder="Szukaj..."
                    value={searchInput}
                    onChange={(e) => setSearchInput(e.target.value)}
                />

                <select
                    value={pageSize}
                    onChange={(e) => {
                        setPageSize(Number(e.target.value));
                        setPage(1);
                    }}
                >
                    <option value={25}>25</option>
                    <option value={50}>50</option>
                    <option value={100}>100</option>
                </select>
            </div>

            {error && <div className="error">{error}</div>}

            <div className="table-wrapper">
                <table>
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Imię</th>
                            <th>Nazwisko</th>
                            <th>Miasto</th>
                            <th>Email</th>
                        </tr>
                    </thead>

                    <tbody>
                        {loading ? (
                            <tr>
                                <td colSpan={5}>Ładowanie...</td>
                            </tr>
                        ) : rows.length === 0 ? (
                            <tr>
                                <td colSpan={5}>Brak danych</td>
                            </tr>
                        ) : (
                            rows.map((row) => (
                                <tr key={row.id}>
                                    <td>{row.id}</td>
                                    <td>{row.firstname}</td>
                                    <td>{row.lastname}</td>
                                    <td>{row.city}</td>
                                    <td>{row.email}</td>
                                </tr>
                            ))
                        )}
                    </tbody>
                </table>
            </div>

            <div className="pagination">
                <button onClick={() => setPage(1)} disabled={page === 1}>
                    Pierwsza
                </button>
                <button onClick={() => setPage((prev) => Math.max(1, prev - 1))} disabled={page === 1}>
                    Poprzednia
                </button>

                <span>Strona {page}</span>

                <button onClick={() => setPage((prev) => prev + 1)} disabled={!hasNextPage || loading}>
                    Następna
                </button>
            </div>
        </div>
    );
}
