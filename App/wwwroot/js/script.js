 buscarDados();

        async function buscarDados() {
            try {
                const response = await fetch('https://localhost:7215/usuarios');
                if (!response.ok) {
                    throw new Error(`Erro: ${response.status}`);
                }
                const dados = await response.json();
                const tabela = document.getElementById("dados");

                tabela.innerHTML = dados.map(usuario => `
                    <tr>
                    <td>${usuario.id}</td>
                    <td>${usuario.nome}</td>
                    <td>${usuario.idade}</td>
                    <td id="apagar"><button type="button" class="btnApagar" onclick="apagarUsuario(${usuario.id})">X</button></td>
                    </tr>
                    `).join("");
            } catch (error) {
                console.error("Erro ao buscar dados:", error);
            }
        }

        async function apagarUsuario(id) {
            if (confirm('Você tem certeza que deseja excluir o usuário?')) {
                try {
                    const response = await fetch(`https://localhost:7215/usuarios/${id}`, {
                        method: "DELETE",
                    });

                    if (!response.ok) {
                        throw new Error(`Erro ao deletar usuário: ${response.status}`);
                    }
                } catch (error) {
                    console.error("Erro na requisição:", error);
                }
            }
            buscarDados();
        }

        document.getElementById("formulario").addEventListener("submit", async function (event) {
            event.preventDefault();

            const nome = document.getElementById("nome").value;
            const idade = parseInt(document.getElementById("idade").value);

            if (nome && idade) {
                const usuario = {
                    "nome": nome,
                    "idade": idade
                };

                if (confirm('Você tem certeza que deseja adicionar o usuário?')) {
                    try {
                        const response = await fetch('https://localhost:7215/usuarios', {
                            method: "POST",
                            headers: {
                                "Content-Type": "application/json"
                            },
                            body: JSON.stringify(usuario)
                        });

                        if (!response.ok) {
                            const error = await response.text();
                            alert(`Erro ao cadastrar usuário: ${error}`);
                        }

                    } catch (error) {
                        console.error("Erro na requisição:", error);
                        alert("Não foi possível enviar os dados. Verifique a conexão com a API.");
                    }
                }
            } else {
                alert("Todos os dados devem ser preenchidos.")
            }

            

            buscarDados();
        });