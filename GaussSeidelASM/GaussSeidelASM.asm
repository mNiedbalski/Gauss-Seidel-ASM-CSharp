
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
            xor     rbx, rbx                   ;Cleaning rbx register for further use as acumulator
            mov     rax, [rsp+40]              ;Storing xArrayTempPtr in rax register
            xorps   xmm0, xmm0                 ;Cleaning xmm0 register
            movdqu  xmm0, [rsp+48]             ;Storing tolerance value in xmm0 register
            xor     r14, r14                   ;Initialize k loop counter in r14 register
    mainLoop: 
            mov     r13b, 1                    ;Initialize boolean variable done in r13b register
            xor     r15, r15                   ;Initialize j loop counter in r15 register with value 0                                  ;Add checking loop condition + resetting j counter
    jLoop:      
            xorps   xmm1, xmm1                 
            movdqu  xmm1, oword ptr[rax+r15]   ;Moving temp value to xmm1 register
            shufps  xmm0, xmm0, 93h            ;Moving tolerance value to the left, so there is space for sum variable
            mov     r10, [rcx]                 ;
            imul    r10, r15                   ;Calculating address of value that will represent sum (equations[j][n])
            add     r10, rcx                   ;So in the end r10 = r8+r15*rcx*8+rcx*8
            imul     r10, 8                     ;
            add     r10, r8                    ;
            movdqu  xmm0, oword ptr[r10]       ;Storing sum variable in xmm0 at 0 index
            xor     r14, r14                   ;Initialize k loop counter in r14 register with value 0
            jmp     kLoop
    incrJ: 
            add     r15, 1                     ;Incrementing j counter
            cmp     r15, rdx                   ;Checking if loop has finished (loops until j==n)
            je      endOfCalc                  ;If j==n, jump to end
            jmp     jLoop                      
    kLoop:      
            cmp     r14, r15                   ;Checking if k==j
            je      incrK                      ;If they are equal, just increment k counter
            mov     r10, r14
            imul     r10, 8
            shufps  xmm1, xmm1, 93h            ;Creating space by moving left for xArray element located at rbx address
            movdqu  xmm1, oword ptr [rax+r10]  ;Placing xArray element in xmm1 slot 0
            mov     r10, rcx

            ;mulss   xmm0, xmm1                 ;equations[j][k] * x[k]


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