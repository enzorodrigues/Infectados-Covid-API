# Infectados-Covid-API

> API.NET de registro de infectados pelo Covid-19 integrado ao MongoDB, utilizando interface de documentação da API com Swagger.

Utilizando CPF como uma forma de identificador para implementação e teste dos métodos GET/{id}, PUT e DELETE.

Para teste:

https://localhost:5001/

#### POST ou PUT

```json
{
  "cpf": 10000000000,
	"Idade": 18,
	"sexo": "M",
	"latitude": -23.5630994,
	"longitude": -46.6565712
}
```

OBS: é preciso ter configurar o acesso ao banco no appsettings.
