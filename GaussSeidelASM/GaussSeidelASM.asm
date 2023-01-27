
; MyProc1(int n, int maxIterations, float equations[][], float x[], float x[], float tolerance)
; POINTERY TO INTY!
; rcx, rdx , r9, r8, stack, stack


; Registers:
;   n: rcx
;   maxIterations: rdx
;   equationsPtr: r8
;   xArrayReturnedPtr: r9
;   xArrayTempPtr: stack [rsp+40]
;   tolerance: stack [rsp+48]
;   
;   
;if (k != j) sum -= equations[j][k] * x[k];
.code

MyProc1 proc
            xor     rax, rax                   ;Cleaning rax register
            mov     rax, [rsp+40]              ;Storing xArrayTempPtr in rax register
            xorps   xmm0, xmm0                 ;Cleaning xmm0 register
            movdqu  xmm0, [rsp+48]             ;Storing tolerance value in xmm0 register
            xor     r15, r15                   ;Initialize j loop counter in r15 register
            xor     r14, r14                   ;Initialize k loop counter in r14 register
mainLoop: 
            mov     r13b, 1                    ;Initialize boolean variable done in r13b register
jLoop:      
            xorps   xmm1, xmm1                 
            movdqu  xmm1, oword ptr[rax+r15]   ;Moving temp value to xmm1 register
            shufps  xmm0, xmm0, 93h            ;Moving tolerance value to the left, so there is space for sum variable
            mov     r10, [rcx]                    ;
            imul    r10, r15
            iadd     r10, rcx
            iadd     r10, r8
            movdqu  xmm0, [r10]

            ret
        
        

;    mov     rdx, [rsp+40]   
;    cmp     rdx, r9
;    je      end1
;    movdqu  xmm0, oword ptr[rcx]
;    mulps   xmm1,xmm2
;    subss   xmm0, xmm1
;    pextrd  dword ptr [rcx], xmm0, 0     ;WYCIAGA DWORDA KTORY JEST NA INDEKSIE 0 W XMM0 (CZYLI NA NAJMLODSZYCH BITACH). TRZECI ARGUMENT DEFINIUJE KTORY BIT, DRUGI DEFINIUJE Z JAKIEGO XMMA WYCIAGAMY A PIERWSZY GDZIE WRZUCAMY
;end1:
                           

MyProc1 endp
end