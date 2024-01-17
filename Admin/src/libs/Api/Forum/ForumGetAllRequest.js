
import WebServiceRequest from '../../Api/WebServiceRequest'

class ForumGetAllRequest extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag(`Forum/GetAllForAdmin?pageNumber=${data.pageNumber}&count=10&searchCommand=${data.search}&noComments=${data.noComments}&mostRated=${data.mostRated}&mostComments=${data.mostComments}&status=${data.status}`)
  }
}
export default ForumGetAllRequest;
