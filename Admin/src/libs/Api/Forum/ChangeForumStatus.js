import WebServiceRequest from '../../Api/WebServiceRequest'

class ChangeForumStatus extends WebServiceRequest {
  constructor (context) {
    super(context)
  }
  setParams(data){
    super.setTag(`Forum/ChangeForumStatus?forumId=${data.forumId}&status=${data.status}`)
  }
}
export default ChangeForumStatus;
