.code
MyProc1 proc

section .data
    done dd 1
    n dd [number of unknowns]
    x dq [pointer to x array]
    equations dq [pointer to equations array]
    tolerance dq [tolerance value]

section .text
solve:
    mov ebx, 0
    mov eax, n
    mov ecx, done

loop1:
    cmp ebx, eax
    jge endloop1

    mov edx, [x + 8*ebx]
    mov [temp], edx

    movsd xmm0, [equations + 8*(eax*ebx + eax)]
    movsd xmm1, [temp]
    mulsd xmm1, [tolerance]
    ucomisd xmm0, xmm1
    jle check_done

    movsd xmm0, 0
    mov [done], xmm0

check_done:

    movsd xmm0, [equations + 8*(eax*ebx + eax)]
    mov edx, 0
    mov ecx, ebx

loop2:
    cmp edx, eax
    jge endloop2

    cmp edx, ebx
    je next_iteration

    mulsd xmm1, [x + 8*edx]
    subsd xmm0, xmm1

next_iteration:
    inc edx
    jmp loop2

endloop2:
    divsd xmm0, [equations + 8*(eax*ebx + ebx)]
    movsd [x + 8*ebx], xmm0
    inc ebx
    jmp loop1

endloop1:
    cmp [done], 1
    je exit
    jmp loop1

exit:
    ret

MyProc1 endp
end