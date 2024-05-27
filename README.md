# AR-Core-Project
### Unity 2022.3.8f1
### AR 프로그래밍 기말 프로젝트
AR - 포탈 - 상호작용 가능 게임 제작

----

### 렌더러 마스크 지정(포탈 제작)
창문 레이어 PortalWIndow - Transparent
맵 레이어 PortalCOntents - Opaque
Project Settings - Quality - Rendering - Render Pipeline Asset -> 포탈 렌더러
Opaque Layer Mast - PortalCOntents만 해제

### 환경 설정
- AR 코어 템플릿 + 애셋(프로젝트 설정은 import하지 않고 실행)


### 사용 애셋
Unity Learn | 3D Beginner: Tutorial Resources | URP


----

### 렌더링 포탈-맵 마스크 문제
- Portal 렌더러 설정값은 의도한대로 문제는 없었으나 게임을 실행하면 메인카메라, 프로젝트 렌더러가 기존 URP-Perfomant로 설정이됨
- 그래서 편집씬에서는 정상적으로 창문을 통해서만 맵을 볼 수 있었으나, 게임을 시작하면 렌더러가 바뀌면서 초기화가 되는 현상
- 이를 해결하기 위해 URP-Perfomant의 Renderer List를 마스크 설정이 된 URP-Portal Asset_Renderer로 설정하여, 렌더러가 URP-Perfomant으로 바뀌어도 문제가 없는 것을 확인. 즉 정상적으로 작동함
- 의문인 점은 URP-Performant-Renderer에서도 Portal Renderer와 같이 설정하였는데 다른 결과를 보인다는 점(이 부분은 따로 확인하지는 않았지만 실행하면 URP-Performant-Renderer여기의 설정들도 바뀔 수 있는 가능성이 높다고 생각)
