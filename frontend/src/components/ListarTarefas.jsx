import { Trash2 } from "lucide-react";
import { CircleCheck } from "lucide-react";
function ListarTarefas({ tarefas }) {

    return (
        <>
        <ul className="space-y-2">
            {tarefas.map((tarefa) => (
                
                <li key={tarefa.id} className="bg-[oklch(28.2%_0.091_267.935)] text-white p-4 rounded-xl flex items-center gap-3">
                    
                    <button
                        className="flex items-center bg-green-400 hover:bg-green-500 px-4 py-2 rounded-sm transition">
                        <CircleCheck size={22} color="white" />
                    </button> 
                
                    <span className="flex-1"> {tarefa.descricao} </span> 

                    <button
                        className="flex items-center bg-red-400 hover:bg-red-500 px-4 py-2 rounded-sm transition">
                        <Trash2 size={12} color="white" />
                    </button>  
                    
                </li>

            ))}
        </ul>
        </>
    );
}
export default ListarTarefas;