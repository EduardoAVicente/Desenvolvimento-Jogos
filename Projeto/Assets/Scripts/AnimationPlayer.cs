using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationPlayer : MonoBehaviour
    {
        [SerializeField]
        private new Rigidbody2D rigidbody2D;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Animator animator;

        private void LateUpdate()
        {
            Vector2 velocidade = this.rigidbody2D.velocity;
            //Debug.Log("y"+velocidade.y);
            //Debug.Log("x"+velocidade.x);


            if (velocidade.x != 0)
            {
                this.animator.SetBool("andando", true);
            }
            else if (velocidade.y > 0.1f) {

                this.animator.SetBool("andando_cima", true);
            }
            else if (velocidade.y < 0)
            {

                this.animator.SetBool("andando_baixo", true);
            }
            else
            {
                this.animator.SetBool("andando", false);
                this.animator.SetBool("andando_cima", false);
                this.animator.SetBool("andando_baixo", false);
            }

            if(velocidade.x > 0)
            {
                this.spriteRenderer.flipX = false;
            }else if(velocidade.x < 0)
            {
                this.spriteRenderer.flipX = true;
            }
        }
    }
}
