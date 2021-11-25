using System.Collections;
using UnityEngine;

public class Arco : MonoBehaviour
{

    public IEnumerator ArcoTrajetoria(Vector3 destino, float duracao) //Trajet�ria da muni��o
    {
        var posicaoInicial = transform.position;
        var percentualCompleto = 0.0f;
        while(percentualCompleto < 1.0f)
        {
            percentualCompleto += Time.deltaTime / duracao;
            var alturaCorrente = Mathf.Sin(Mathf.PI * percentualCompleto);
            transform.position = Vector3.Lerp(posicaoInicial, destino, percentualCompleto) + Vector3.up*alturaCorrente;
            yield return null;
        }
        gameObject.SetActive(false);
    }

}
