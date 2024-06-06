# AR-Core-Project
### Unity 2022.3.8f1
### AR 프로그래밍 기말 프로젝트
----

## 게임 진행 방법
1. AR Plane을 벽을 인식   
![1 AR 인식](https://github.com/toproof25/AR_Project/assets/41888060/49a882af-f24f-4052-997f-e80292c82b1a)

  
2. 터치를 통해 포탈을 생성(포탈을 통해서만 맵을 볼 수 있다)
3. 조이스틱으로 플레이어를 이동   
![2 포탈 생성 후 조이스틱 이동](https://github.com/toproof25/AR_Project/assets/41888060/f3784539-fcb1-4222-8321-b8b2062ee76d)

  
4. 열쇠를 획득   
![3  아이템 획득](https://github.com/toproof25/AR_Project/assets/41888060/203cb4d2-41ae-407f-bcc5-29e587334624)


5. 몬스터를 피해서 이동(공격 받으면 목숨이 깎인다)   
![4  공격 피격](https://github.com/toproof25/AR_Project/assets/41888060/550f63a7-6e1a-4b15-a772-ef9eed111e80)


6. 탈출 지점까지 이동   
![5  탈출](https://github.com/toproof25/AR_Project/assets/41888060/2135e596-c5f8-4847-9ef0-868ecf6d6d15)



## 환경 설정
- AR Core 템플릿으로 프로젝트 생성
- 3D Beginner: Tutorial Resources 에샛을 추가할 때 그냥 import하면 프로젝트 설정도 모두 오버라이드가 된다.
- 문제는 오버라이드를 하면 AR Core 설정도 변경되어 AR이 제대로 작동하지 않음.
- 고로 Project Setting은 체크 해제 후 모두 import


## 사용 애셋
- [Unity Learn | 3D Beginner: Tutorial Resources | URP](https://assetstore.unity.com/packages/essentials/tutorial-projects/unity-learn-3d-beginner-complete-project-urp-143846)
- [Zombie](https://assetstore.unity.com/packages/3d/characters/humanoids/zombie-30232)
- [Free Zombie Character Sounds](https://assetstore.unity.com/packages/audio/sound-fx/creatures/free-zombie-character-sounds-141740)
- [Rust Key](https://assetstore.unity.com/packages/vfx/particles/hit-impact-effects-free-218385)
- [Hit Impact Effects FREE](https://assetstore.unity.com/packages/vfx/particles/hit-impact-effects-free-218385)


### 참고 문서
- [조이스틱 이동](https://wergia.tistory.com/231)

### 제작한 스크립트 (용도, 기능)
- ZombieAttack (좀비 몬스터의의 공격)
- SetMoveMap (맵 위치 이동)
- SendMessge (메시지 출력)
- LookAtCamera (UI 카메라 바라보기)
- Joystick (조이스틱 이동)
- ItemScript (열쇠 획득)
- GameStart (게임 포탈 생성 및 설정 초기화)

### 기존 애셋을 수정한 스크립트 (수정 내용)
- Observer (OnTriggerEnter가 되면 좀비 공격 수행하도록 수정)
- PlayerMovement (조이스틱 위치와 카메라 방향에 따라 이동하도록 변경)
- GameEnding (게임 종료 조건을 목숨 3번으로 변)
----

### 수정한 내용

- [x] AR터치 시 창문 스폰
	- 기존 플레이스 온 플랜 컴포넌트 사용   
- [x] 창문을 통해서만 맵을 볼 수 있게 구현 
	- 레이어 마스크를 통해 구현  
- [x] 재시작 시 씬 로드가 아닌 위치 초기화
	- Game Ending스크립트를 수정하여 사용  
- [x]  플레이어 이동 구현(조이스틱)
	- 조이스틱 UI를 통해 사용  
- [x] 게임 시작 (포탈 생성) 이후 조이스틱 및 UI가 보이게 구현
	- PlaceOnPlane을 GameStart로 변경 후 시작 시 Canvas.SetActive(True)  
- [x] 네비매쉬 동적 베이크 (현재 맵 이동 시 적들이 움직이지 않음->내비메쉬 문제)
	- 해당 부분에서 적이 제대로 이동하지 않는 현상이 발생
		- 해결: 기존 베이크된(애셋폴더) 내비메쉬를 지우고, 바닥 오브젝트에 NavMeshSurface를 사용하여 동적 베이크? 사용
		- 또 셋맵 버튼 누를 시 베이크된 맵 다시 로드?하고 몬스터 NavMeshAgent에도 다시 적용  
- [x] 조이스틱 부분은 AR레이가 적용 안되게 해야함
	- 해결: 클릭 시 레이를 이용하여 UI인지 구분 후 실행  
- [x] 카메라 기준으로 상하좌우 이동 구현
	- 해결: 카메라 transform을 이용하여 방향에 맞게 이동 수정  
- [x] 적 오브젝트도 창문을 통해서만 보이도록 수정
	- 해결: 유령, 빛 등이 투명한 오브젝트로 Transparent 렌더링이됨 투명한 오브젝트들을 모두 불투명한 Opaque로 바꾸어서 적용  
- [x] 아이템 주워서 탈출
	- 키 아이템을 주우면 탈출할 수 있다
	- ItemScript : 아이템에 트리거엔터가 되면 GameEnding에 getItem이 true가됨
	- GameEnding : 탈출 오브젝트 트리거엔터시 getItem도 true여야 탈출 성공
	- SendMessege : 게임에서 공지, 메시지를 전달함(싱글)   
- [x] 목숨을 3개로 변경
	- GameEnding : m_lives 정수 3으로 목숨을 구현
	- Lives : 목숨 UI로 스크롤 뷰로 구성, 1개 깎이면 SetActive(false)가  
- [x] 적을 유령이 아닌 좀비로 변경
	- 애니메이션 믹사모 좀비 워크로 변경
	- 걷는 소리도 바꾸면 좋을 듯 (으어어~ 소리, 좀비 걷는 소리)
	- 플레이어 닿으면 공격?
		- 믹사모 애니메이션 사용

----
### 렌더러 마스크 지정(포탈을 통해서만 맵이 보이도록 제작)
- 창문 레이어 PortalWIndow - Transparent  
- 맵 레이어 PortalCOntents - Opaque  
- Project Settings - Quality - Rendering - Render Pipeline Asset -> 렌더러 설정 
- Opaque Layer Mask - PortalContents만 해제
- 모든 오브젝트를 Opaque로만 설정하여 깔끔하게 사라지도록 구현

### 렌더링 포탈-맵 마스크 문제 해결
- Portal 렌더러 설정값은 의도한대로 문제는 없었으나 게임을 실행하면 메인카메라, 프로젝트 렌더러가 기존 URP-Perfomant로 설정이됨
- 그래서 편집씬에서는 정상적으로 창문을 통해서만 맵을 볼 수 있었으나, 게임을 시작하면 렌더러가 바뀌면서 초기화가 되는 현상
- 이를 해결하기 위해 URP-Perfomant의 Renderer List를 마스크 설정이 된 URP-Portal Asset_Renderer로 설정하여, 렌더러가 URP-Perfomant으로 바뀌어도 문제가 없는 것을 확인. 즉 정상적으로 작동함
- 의문인 점은 URP-Performant-Renderer에서도 Portal Renderer와 같이 설정하였는데 다른 결과를 보인다는 점(이 부분은 따로 확인하지는 않았지만 실행하면 URP-Performant-Renderer여기의 설정들도 바뀔 수 있는 가능성이 높다고 생각)
