
; MyProc1(int n, int maxIterations, float equations[][], float x[], float tolerance)
; POINTERY TO INTY!
; rcx, rdx , r9, r8, stack, stack


; Registers:
;   n: rcx
;   maxIterations: rdx
;   equationsPtr: r8
;   xArrayReturnedPtr: r9
;   tolerance: stack [rsp+40]
;   
;   
;if (k != j) sum -= equations[j][k] * x[k];
.code

MyProc1 proc        
            movss   xmm0, dword ptr[rsp+40]    ;Storing tolerance value in xmm0 register
            xor     r14, r14                   ;Initialize k loop counter in r14 register
    mainLoop: 
            mov     r13b, 1                    ;Initialize boolean variable done in r13b register
            xor     r15, r15                   ;Initialize j loop counter in r15 register with value 0
    jLoop:      
            xorps   xmm1, xmm1                 
            movss   xmm1, dword ptr[r9+r15]    ;Moving temp value to xmm1 register
            mov     r10, rcx                   ;
            imul    r10, r15                   ;Calculating address of value that will represent sum (equations[j][n])
            add     r10, rcx                   ;So in the end r10 = r8+r15*rcx*4+rcx*4
            imul    r10, 4                     ;
            add     r10, r8                    ;


            ;##################################  REPAIR IT SO IT DOESNT NEED TO USE XMM2 TO NOT AFFECT BITS 9-16 OF XMM0  #############################################
            shufps  xmm0, xmm0, 93h            ;Moving tolerance value to the left, so there is space for sum variable XMM0 = [][][tolerance][]
            movss   xmm2, dword ptr[r10]       ;Getting sum variable to xmm2, so it doesnt break stuff in xmm0
            movss   xmm0,xmm2                  ;Storing sum variable in xmm0 at 0 index XMM0 = [][][tolerance][sum]
            ;##################################  REPAIR IT SO IT DOESNT AFFECT BITS 9-16  #############################################

            cmp     r14, rcx
            je      afterKLoop
            xor     r14, r14                   ;Initialize k loop counter in r14 register with value 0
            jmp     kLoop

    afterKLoop:
            

    incrJ: 
            add     r15, 1                     ;Incrementing j counter
            cmp     r15, rdx                   ;Checking if loop has finished (loops until j==n)
            je      endOfCalc                  ;If j==n, jump to end

            ;x[j] = sum / equations[j][j]; // obliczanie nowej wartoœci nieznanej      
            ;if (Math.Abs(x[j] - temp) > tolerance) done = false;

            jmp     jLoop                      
    kLoop:      
            cmp     r14, r15                   ;Checking if k==j
            je      incrK                      ;If they are equal, just increment k counter
            mov     r10, r14
            imul    r10, 4
            shufps  xmm1, xmm1, 93h            ;Creating space by moving left for xArray element located at rbx address (temp now in slot 1)
            movss   xmm1, dword ptr [r9+r10]   ;Placing xArray element in xmm1 slot 0
            mov     r10, rcx
            imul    r10, r15
            add     r10, r14
            imul    r10, 4
            add     r10, r8

;##################################  REPAIR IT SO IT DOESNT NEED TO USE XMM2 TO NOT AFFECT BITS 9-16 OF XMM0  #############################################
            shufps  xmm0, xmm0, 93h            ;Moving sum variable to the left, so now there is space for equations[j][k] XMM0 = [][tolerance][sum][]   
            movss   xmm2, dword ptr [r10]      ;Storing equations[j][k] in xmm2
            movss   xmm0, xmm2                 ;Storing equations[j][k] | XMM0 = [][tolerance][sum][equations[j][k]]
;##################################  REPAIR IT SO IT DOESNT NEED TO USE XMM2 TO NOT AFFECT BITS 9-16 OF XMM0  #############################################
            
            mulss   xmm1, xmm0                 ;equations[j][k] * x[k]
            shufps  xmm0, xmm0, 39h            ;Moving equations[j][k] to right (slot3) so now sum is on slot 0 XMM0 = [equations][][tolerance][sum]
            subss   xmm0, xmm1                 ;sum=sum-equations[j][k]*x[k]
            shufps  xmm1, xmm1,39h             ;Moving temp right to slot 0, so when loopK iterates again, it meets the same state of slot 0 in register xmm1 XMM1 = [xArray][][][temp]
    incrK:
            add     r14, 1                      ;Incrementing k counter
            cmp     r14, rcx                    ;Checking if loop has finished (loops until k==n)
            je      incrJ                       ;Coming back to jLoop
            jmp     kLoop                       ;Coming back to loop



    endOfCalc:  
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