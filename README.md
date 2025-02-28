# Unity_Small_Project
 2~3일짜리 단기 프로젝트

### 기획서?
https://peppermint-waxflower-91d.notion.site/1a6bf749df0680dcb1b5c5e281798b37?pvs=4 


## 1일차
- 플레이어 움직임(좌우, 점프) 구현
-  카메라 추적 구현, 플레이어 스프라이트 좌우 전환 구현
-  적 이동(AI) 1일차 구현

## 2일차
- 몬스터 움직임 ai 로직 수정 
  - 문제: 몬스터의 trigger enter와 trigger exit를 동시에 사용할 경우, 동시에 불리는 경우가 생김
  - 해결 방법: 몬스터는 바닥에 있어야 하니까 바닥레이어를 벗어나는 조건으로 통일화 시킴
- 플레이어 죽음 로직 구현
- 플레이어 죽음 디테일 챙기기
  - 죽었을때 위로 튐, 죽어서 충돌 제거, 죽고 반투명 빨간색
- 플레이, 적 에니메이션 구현
- 일반 블럭과 아이템 생성 블록 구현, 마리오 버섯 기믹 구현
- 아이템 블록 디테일한 구현과 코인 점수 증가 구현
- 포탈 구현
- 오류수정
  - 적이 죽었을때 동시에 죽는것을 attck_ray.collider.gameObject 를 이용해 충돌한 게임 오브젝트의 컴포넌트 불러와서를 이용해 수정함
- 정수형으로 관리하던거 enum열거형으로 정리
- 리스폰 구현
- 마리오 깃발 로직 구현

## 3일차
- 시작(게임 시작화면) 씬 구현
- 엔딩 텍스트 페이드인 생성 구현
- 오디오 작업
- 코인 콜라이더를 트리거를 활성화해 플레이어가 자연스럽게(버벅임 없이) 이동하도록 수정
- 조금의 레벨디자인, 블럭오류 수정(위의 블럭이 부서지는 판정에서, 아래쪽에 있는 블럭도 부서지는 오류)
- 점프 로직을 레이캐스트를 이용해서 제한을 걸어둠 (이전에는 벽, 옆과 바닥 충돌시 무한 점프가 가능했었음.)
- 적 ai로직 오류 수정(적이 일정한 확률로 바닥을 벗어나서 떨어짐)
- 종료 오류(씬이 종료 코틀린이 불러와지지가 않음. 문제점: update에 종료 코틀린 함수를 넣었는데 프레임마다 불려와서 오류가 발생. 해결법: bool값을 하나 줘서 코틀린 함수가 종료될때까지 bool값이 변하지 않아 계속 불려오지 않게 됨.) 수정, 씬의 코인 배치 수정
- 기획서? 노션 연결


### 시작화면
![시작화면](https://github.com/user-attachments/assets/2bd7115b-546b-4080-9151-d94daa66f95d)

### 마리오의 블럭과 캐릭터가 커지는 버섯 기믹
![_-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/114c6f41-4840-45e3-9318-fdb58da2845a)

### 마리오 버섯 적의 기믹
![__-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/d3e97a8b-dd19-4b37-94a7-ab1b52ba0b95)


### 코인블럭, 배관포탈, 코인개수 증가
![___-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/c4493148-48fe-4de8-82b1-39cf0af15673)


### 깃발, 게임클리어 기믹
![_-ezgif com-video-to-gif-converter (1)](https://github.com/user-attachments/assets/a9875077-fe33-486e-844f-bcae2e24fd16)


