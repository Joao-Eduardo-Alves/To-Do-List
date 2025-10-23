import { Trash2 } from "lucide-react";
function ListarTarefas({ tarefas }) {

    return (
        <>
        <ul className="space-y-2">
            {tarefas.map((tarefa) => (
                <li key={tarefa.id} className="bg-[oklch(28.2%_0.091_267.935)] text-white p-4 rounded-xl flex justify-between items-center gap-3">
                    <span> {tarefa.descricao} </span> 
                    <button
                        className="flex items-center bg-red-300 hover:bg-red-400 px-4 py-2 rounded-sm transition">
                        <Trash2 size={18} color="white" />
                    </button>  
                </li>
            ))}
        </ul>
        </>
    );
}
export default ListarTarefas;