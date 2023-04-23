using ApiTemplate.DTO.Respon;
using ApiTemplate.Services.Interfaces;

namespace ApiTemplate.Services
{
    public class RenglonesService : IRenglonesService
    {
        public async Task<GenericRespon> Get(int number)
        {
            return await Task<GenericRespon>.Factory.StartNew(() =>
            {
                
                int numebrImp = 0, temp = 1;
                List<Stack<int>> list = new List<Stack<int>>();
                
                for (int i = 0; i < number; i++)
                {
                    Stack<int> matriz = new Stack<int>();
                    while(numebrImp <= i)
                    {
                        matriz.Push(temp);
                        temp += 2;
                        numebrImp++;
                    }
                    temp = 1;
                    numebrImp = 0;
                    list.Add(matriz);
                }
                return new GenericRespon()
                {
                    State = 200,
                    Message = "Generado Correctamente",
                    Data = list
                };
            });
        }

        public async Task<GenericRespon> GetRescursive(int number)
        {
            return await Task<GenericRespon>.Factory.StartNew(() =>
            {
                Stack<Stack<int>> list = new Stack<Stack<int>>();
                Stack<int> matriz = new Stack<int>();
                recursivo(number, 1, 1, list, matriz);
                return new GenericRespon()
                {
                    State = 200,
                    Message = "Generado Correctamente",
                    Data = list
                };
            });
        }

        private int recursivo(int renglones, int temp, int contador, Stack<Stack<int>> list, Stack<int> cola)
        {
            if (renglones <= 0)
                return 0;

            if (contador > renglones)
            {
                list.Push(cola);
                cola = new Stack<int>();
                renglones--;
                return recursivo(renglones, 1, 1,list, cola);
            }

            cola.Push(temp);
            contador++;
            temp += 2;
            
            return recursivo(renglones, temp, contador, list, cola);
        }
    }
}
