(define-module (uo cliloc)
  #:use-module (rnrs bytevectors)
  #:use-module (ice-9 binary-ports)
  #:use-module (ice-9 pretty-print)
  #:use-module (uo reader)
  #:use-module (uo writer))

(define-public (read-cliloc-header port)
  (list (read-s32 port) (read-s16 port)))

(define (read-cliloc-flags port)
  (let ((flag (read-u8 port)))
    (cond
     ((eq? flag 0) 'original)
     ((eq? flag 1) 'custom)
     ((eq? flag 2) 'modified)
     (else (throw 'unknown-cliloc-flags)))))

(define (write-cliloc-flag port flag)
  (write-u8 port (case flag
                   ((original) 0)
                   ((custom) 1)
                   ((modified) 2))))

(define-public (read-cliloc-entry port)
  (if (eof-object? (lookahead-u8 port))
      (eof-object)
      `(cliloc
        ,(read-s32 port)
        ,(read-cliloc-flags port)
        ,(read-string port))))

(define-public (write-cliloc-header port header)
  (write-s32 port (car header))
  (write-s16 port (cadr header)))

(define-public (write-cliloc-entry port entry)
  (write-s32 port (cadr entry))
  (write-cliloc-flag port (caddr entry))
  (write-string port (cadddr entry)))

(define-public (dump-clilocs input output)
  (pretty-print (read-cliloc-header input) output)
  (let loop ((entry (read-cliloc-entry input)))
    (unless (eof-object? entry)
      (pretty-print entry output)
      (loop (read-cliloc-entry input)))))

(define-public (compile-clilocs input output)
  (write-cliloc-header output (read input))
  (let loop ((entry (read input)))
    (unless (eof-object? entry)
      (write-cliloc-entry output entry)
      (loop (read input)))))
