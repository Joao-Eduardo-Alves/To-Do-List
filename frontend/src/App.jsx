import { useState } from 'react'
import CadastrarTarefa from './components/CadastrarTarefa.jsx'
import ListarTarefas from './components/ListarTarefas';
function App() {

    const [tarefas, setTarefas] = useState([]);

    const addTarefa = (novaTarefa) => {

        const tarefa = {
            id: Date.now(),
            descricao: novaTarefa,
        };
        setTarefas([...tarefas, tarefa]);
    };  


    return (
        <div className="flex flex-col gap-15 items-center justify-start h-screen bg-gradient-to-t from-blue-300 to-blue-900">
            <CadastrarTarefa adicionar={addTarefa} />
            <ListarTarefas tarefas={tarefas} />
        </div>
    );  
}

export default App
