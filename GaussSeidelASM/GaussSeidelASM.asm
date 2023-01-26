
; przyjmowane parametry:
; MyProc1(float[] neededEquation, int n, ,)
;
; Parameters:

;   equations[j][k]: coefficient from eq
;   x: coefficientX
;   j: value of variable j passed to the function
;   n: value of variable n passed to the function
;   sum: value of variable sum passed to the function

; Registers:
;   sum: XMM0
;   eqJK: XMM1
;   eqX: XMM2
;   j: R9
;   k: stack
;   
;if (k != j) sum -= equations[j][k] * x[k];
.code

MyProc1 proc
    mov     rcx, [rbp + 48]
    cmp     rcx, r9
    je      end1
    mulps   xmm1,xmm2
    subss   xmm0, xmm1
end1:
    ret                       

MyProc1 endp
end