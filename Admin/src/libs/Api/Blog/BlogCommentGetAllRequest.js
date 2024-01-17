import WebServiceRequest from '../../Api/WebServiceRequest'

class BlogCommentGetAllRequest extends WebServiceRequest {
    constructor (context) {
        super(context)
    }
    setParams(data){
        super.setTag('BlogComment/GetAll?pageNumber='+data.pageNumber+'&count='+data.count +'&searchCommand='+ data.search +'&IsActive='+data.IsActive)
    }
}
export default BlogCommentGetAllRequest;
