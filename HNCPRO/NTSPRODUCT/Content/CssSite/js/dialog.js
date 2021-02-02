$(document).ready(function() {
    $('a.signup-window').click(function() {

        //lấy giá trị thuộc tính href - chính là phần tử "#signup-box"
        var signupBox = $(this).attr('href');

        //cho hiện hộp đăng nhập trong 300ms
        $(signupBox).fadeIn("slow");

        // thêm phần tử id="over" vào cuối thẻ body
        $('body').append('<div id="over"></div>');
        $('#over').fadeIn(300);
        
        return false;
    });

    // khi click đóng hộp thoại
    $(document).on('click', "a.close, #over", function() { 
        $('#over, .signup').fadeOut(300 , function() {
            $('#over').remove();  
        }); 
        return false;
    });
	
});


