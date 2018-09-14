<?php
namespace Home\Controller;

use Think\Controller;
header("Content-Type: text/html;charset=utf-8");
header('Access-Control-Allow-Origin:*'); // 允许跨域
class UserController extends Controller
{
	public function index()
	{
		$map=$_GET;
		$model=D("User");
		//$data=$model->where($map)->select();
		$condition['id']=array('gt',0);
		$data=$model->where($condition)->select();
		$this->ajaxReturn($data);
	}

	public function login()
	{
		$model=D("User");
		$info=$_POST;
		$condition['username'] = $_POST['username'];
		$query['username'] = $_POST['username'];
		$query['password'] = $_POST['password'];

		$uid = $model->where($condition)->select()[0]['id'];
		$uname = $model->where($condition)->select()[0]['username'];

		$result = $model->where($condition)->count();
		if($result == 0){
			$data=array(
				'code' => '102',
				'msg' => '未找到该用户'
			);
			$this->ajaxReturn($data);
		}else{
			$result1 = $model->where($query)->count();

			if($result1>0){
				$data=array(
					'code'=>'0',
					'msg'=>'登录成功',
					'uid'=>$uid,
					'uname'=>$uname
				);
			}else{
				$data=array(
					'code'=>'103',
					'msg'=>'密码错误'
				);
			}
			$this->ajaxReturn($data);
		}
	}

	public function register(){
		$model=D("User");
		$condition['username'] = $_POST['username'];
		$isExist = $model->where($condition)->count();
		$data = ['code'=>'101','msg'=>"注册返回信息"];
		if($isExist>0)
		{
			$data['code']='102';
			$data['msg']='用户名已被注册';
		}else{
			$uInfo = $_POST;
			$id = $model->Max('id');
			$id = (int)$id + 1; // 内置函数 intval($id); 也可以，但 (int)$id; 强制类型转换最优
			$uInfo['id']=$id;
			$result=$model->add($uInfo);
			if($result > 0){
				$data['code']='0';
				$data['msg']='注册成功';
			}else{
				$data['code']='102';
				$data['msg']='注册失败';
			}
		}
		
		$this->ajaxReturn($data);
	}
}