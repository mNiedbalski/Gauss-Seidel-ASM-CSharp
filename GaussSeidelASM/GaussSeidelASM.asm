
; MyProc1(sumPtr, equations[j][k], x[k], j, k);
; rcx, xmm1, xmm2, r9, stack
; Parameters:

;   equations[j][k]: coefficient from eq
;   x: coefficientX
;   j: value of variable j passed to the function
;   n: value of variable n passed to the function
;   sum: value of variable sum passed to the function

; Registers:
;   sum: XMM0  / sumPtr: RCX
;   eqJK: XMM1
;   eqX: XMM2
;   j: R9
;   k: stack
;   
;if (k != j) sum -= equations[j][k] * x[k];
.code

MyProc1 proc
    mov     rdx, [rsp+40]   
    cmp     rdx, r9
    je      end1
    movdqu  xmm0, oword ptr[rcx]
    mulps   xmm1,xmm2
    subss   xmm0, xmm1
    pextrd  dword ptr [rcx], xmm0, 0
end1:
    ret                       

MyProc1 endp
end