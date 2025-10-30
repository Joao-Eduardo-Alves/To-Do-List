import { useState } from "react";

function CadastrarTarefa({ adicionarTarefa }) {

    const [descricao, setText] = useState("");

    const handleAdd = () => {
        adicionarTarefa(descricao);
        setText("");
    };

    return (
        <div className= "rounded-xl w-100 h-40 bg-[oklch(28.2%_0.091_267.935)] flex flex-col gap-10 ">
            <h1 className="text-white text-center font-bold text-4xl">Cadastrar tarefa</h1>
            <div className="flex gap-4 justify-center">
                
            <input
                type="text"
                placeholder="Digite uma tarefa..."
                onChange={(e) => setText(e.target.value)}
                value={descricao} 
                className="bg-gray-700 text-white p-3 rounded-xl  focus:outline-none"
            />
            
            <button
                onClick={handleAdd}
                className="bg-blue-500 hover:bg-blue-600 px-4 py-2 rounded-xl transition"
            >
                Adicionar
                </button>
            </div>
        </div>
    );
}

export default CadastrarTarefa;
