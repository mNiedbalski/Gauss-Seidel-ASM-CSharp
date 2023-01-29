
; MyProc1(int n, int maxIterations, float equations[][], float x[], float tolerance)
; POINTERY TO INTY!
; rcx, rdx , r9, r8, stack, stack


; Registers:
;   n: rcx
;   maxIterations: rdx
;   equationsPtr: r8
;   xArrayReturnedPtr: r9
;   tolerance: stack [rsp+40]
;   n+1: r12
;   
;if (k != j) sum -= equations[j][k] * x[k];
.code

MyProc1 proc        
            movss   xmm1, dword ptr[rsp+40]    ;Storing tolerance value in xmm1 register
            mov     r11, rdx
            mov     r12, rcx
            inc     r12
            xor     r14, r14                   ;Initialize k loop counter in r14 register
    mainLoop: 
            cmp     r13b, 1
            je      endOfCalc
            dec     r11
            cmp     r11, 0
            je      endOfCalc
            mov     r13b, 1                    ;Initialize boolean variable done in r13b register
            xor     r15, r15                   ;Initialize j loop counter in r15 register with value 0
    jLoop:                 
            movss   xmm2, dword ptr[r9+r15*4]    ;Moving temp value to xmm2 register
            shufps  xmm1, xmm1, 93h            ;XMM1 = [][][tolerance][]
            movss   xmm1, xmm2                 ;XMM1 = [][][tolerance][temp]

            mov     r10, r12                   ;
            imul    r10, r15                   ;Calculating address of value that will represent sum (equations[j][n])
            add     r10, rcx                   ;So in the end r10 = r8+r15*rcx*4+rcx*4
            imul    r10, 4                     ;
            add     r10, r8                    ;
            movss   xmm0, dword ptr[r10]       ;XMM0 = [][][][sum]
            xor     r14, r14                   ;Initialize k loop counter in r14 register with value 0
            jmp     kLoop

    afterKLoop:         ;first x[j] = sum / equations[j][j] then if (Math.Abs(x[j] - temp) > tolerance) done = false;
            
            ;XMM0 = [equations[j][k]][][][sum] XMM1 = [x[k]][][tolerance][temp]
            mov     r10, r15                   ;Storing j counter in r10
            imul    r10, r15                   ;
            add     r10, r15                   ;Getting address equations[j][j] = r8+(r15*r15+r15)*4 
            imul    r10, 4                     ;
            movss   xmm2, dword ptr[r8+r10]    ;XMM2 = [][][][equations[j][j]]
            divss   xmm0, xmm2                 ;XMM0 = [equations[j][k]][][][x[j]] 
            shufps  xmm1, xmm1, 93h            ;XMM1 = [][tolerance][temp][x[k]]
            movss   xmm1, xmm0                 ;XMM1 = [][tolerance][temp][x[j]]
            pextrd  dword ptr [r9+r15*4], xmm0, 0   ;Saving x[j]=sum/equations[j][j] value in memory
            ;movss   xmm0, xmm1                 ;XMM0 = [equations[j][k]][][][x[j]] MAYBE NOT NEEDED, BUT IDK IF pextrd FULLY EXTRACTS OR NOT

            xorps   xmm2, xmm2                 ;Clearing for use in comparing to 0
            shufps  xmm1, xmm1, 39h            ;XMM1 = [x[j]][][tolerance][temp]
            subss   xmm0, xmm1                 ;x[j]-temp
            comiss  xmm0, xmm2                 ;
            jl      Negative      ;jl          ;jump if x[j]-temp<0
    AbsDone:
            shufps  xmm1, xmm1, 39h            ;XMM1 = [temp][x[k]][][tolerance]
            comiss  xmm0, xmm1                 ;
            jg      doneFalse  ;jg             ;if x[j]-temp > tolerance
    incrJ: 
            inc     r15                        ;Incrementing j counter
            cmp     r15, rcx                    ;Checking if loop has finished (loops till j<n)                 
            je      mainLoop                   ;If j==n, jump back to mainLoop
            jmp     jLoop
    Negative:
            movss   xmm2, xmm0
            subss   xmm0, xmm0
            subss   xmm0, xmm2
            jmp AbsDone
            
    doneFalse:
            mov     r13b,0                     ;done=false
            jmp     incrJ

    kLoop:      
            cmp     r14, r15                   ;Checking if k==j
            je      incrK                      ;If they are equal, just increment k counter
            mov     r10, r14
            imul    r10, 4
            shufps  xmm1, xmm1, 93h            ;XMM1 = [][tolerance][temp][]
            movss   xmm2, dword ptr [r9+r10]   ;Storing temporarily xArray element in xmm2
            movss   xmm1, xmm2                 ;XMM1 = [][tolerance][temp][x[k]]
            mov     r10, r12
            imul    r10, r15
            add     r10, r14
            imul    r10, 4
            add     r10, r8
            shufps  xmm0, xmm0, 93h            ;Moving sum variable to the left, so now there is space for equations[j][k] XMM0 = [][][sum][]   
            movss   xmm2, dword ptr [r10]      ;Storing equations[j][k] in xmm2
            movss   xmm0, xmm2                 ;XMM0 = [][][sum][equations[j][k]]
            mulss   xmm1, xmm0                 ;x[k] * equations[j][k]
            shufps  xmm0, xmm0, 39h            ;XMM0 = [equations][][][sum]
            subss   xmm0, xmm1                 ;sum=sum-equations[j][k]*x[k]
            shufps  xmm1, xmm1,39h             ;XMM1 = [x[k]][][tolerance][temp]
    incrK:
            inc     r14                        ;Incrementing k counter
            cmp     r14, rcx                   ;Checking if loop has finished (loops until k<n)
            je      afterKLoop                 ;Coming back to jLoop
            jmp     kLoop                      ;Coming back to loop

    endOfCalc:
            xorps xmm4, xmm4
            movss xmm4, dword ptr [r9]
            movss xmm4, dword ptr [r9+4]
            movss xmm4, dword ptr [r9+8]
            ret
MyProc1 endp
end