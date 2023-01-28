
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
            movss   xmm1, dword ptr[rsp+40]    ;Storing tolerance value in xmm1 register
            xor     r14, r14                   ;Initialize k loop counter in r14 register
    mainLoop: 
            mov     r13b, 1                    ;Initialize boolean variable done in r13b register
            xor     r15, r15                   ;Initialize j loop counter in r15 register with value 0
    jLoop:                       
            movss   xmm2, dword ptr[r9+r15]    ;Moving temp value to xmm2 register
            shufps  xmm1, xmm1, 93h            ;XMM1 = [][][tolerance][]
            movss   xmm1, xmm2                 ;XMM1 = [][][tolerance][temp]

            mov     r10, rcx                   ;
            imul    r10, r15                   ;Calculating address of value that will represent sum (equations[j][n])
            add     r10, rcx                   ;So in the end r10 = r8+r15*rcx*4+rcx*4
            imul    r10, 4                     ;
            add     r10, r8                    ;
            movss   xmm0, dword ptr[r10]       ;XMM0 = [][][][sum]
            xor     r14, r14                   ;Initialize k loop counter in r14 register with value 0
            jmp     kLoop

    afterKLoop:
            shufps  xmm0, xmm0, 39h            ;XMM0 = [sum][equations][][]
            movss   xmm2, dword ptr[r9+15]     ;Loading x[j] into xmm2
            movss   xmm0, xmm2                 ;XMM0 = [sum][equations][][x[j]]
            xorps   xmm2, xmm2                 ;Clearing for use in comparing to 0
            subss   xmm0, xmm1                 ;x[j]-temp
            ;cmpss   xmm0, xmm2                 ;;######################CORRECT################################
            ;jl      Negative                   ;jump if x[j]-temp<0
            shufps  xmm1, xmm1, 39h            ;XMM1 = [temp][x[k]][][tolerance]
            ;cmpss   xmm0, xmm1                 ;#######################CORRECT###############################
            ;jg      doneFalse                  ;if x[j]-temp > tolerance
            jmp     jLoop
    incrJ: 
            add     r15, 1                     ;Incrementing j counter
            cmp     r15, rdx                   ;Checking if loop has finished (loops until j==n)
            je      endOfCalc                  ;If j==n, jump to end
            jmp     jLoop
    Negative:
            ;;mulss   xmm0, -1                       ;ABS[xmm0[0]]#######################################

    doneFalse:
            mov     r13b,0                     ;done=false
            jmp     jLoop

    kLoop:      
            cmp     r14, r15                   ;Checking if k==j
            je      incrK                      ;If they are equal, just increment k counter
            mov     r10, r14
            imul    r10, 4
            shufps  xmm1, xmm1, 93h            ;XMM1 = [][tolerance][temp][]
            movss   xmm2, dword ptr [r9+r10]   ;Placing xArray element in xmm1 slot 0
            movss   xmm1, xmm2                 ;XMM1 = [][tolerance][temp][x[k]]
            mov     r10, rcx
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
            add     r14, 1                      ;Incrementing k counter
            cmp     r14, rcx                    ;Checking if loop has finished (loops until k==n)
            je      afterKLoop                  ;Coming back to jLoop
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